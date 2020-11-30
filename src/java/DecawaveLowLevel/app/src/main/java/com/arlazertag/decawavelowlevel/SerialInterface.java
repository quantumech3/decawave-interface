package com.arlazertag.decawavelowlevel;

import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.hardware.usb.UsbDeviceConnection;
import android.hardware.usb.UsbManager;

import com.hoho.android.usbserial.driver.CdcAcmSerialDriver;
import com.hoho.android.usbserial.driver.ProbeTable;
import com.hoho.android.usbserial.driver.UsbSerialDriver;
import com.hoho.android.usbserial.driver.UsbSerialPort;
import com.hoho.android.usbserial.driver.UsbSerialProber;
import com.hoho.android.usbserial.util.SerialInputOutputManager;

import java.io.IOException;
import java.util.List;
import java.util.concurrent.Executors;

public class SerialInterface  {
    private static final String TAG = "DecawaveLowLevel-Java";

    private static UnityCallback unityCallback;
    private static Context unityContext;
    private static UsbManager usbManager;
    private static UsbSerialDriver deviceDriver;
    private static UsbDeviceConnection usbDeviceConnection;
    private static UsbSerialPort usbSerialPort;

    //Function called from the C#-side to register callbacks
    public static void setCallbackListener(UnityCallback callback) {
        unityCallback = callback;
    }

    //Function called from the C#-side to initialize usbManager and deviceDriver objects
    //Returns true on success, returns false on failure (device not found / in-use)
    public static boolean initializeJavaSide(Context context, int vendorID, int productID) {
        //Save the context for later (when we request permissions)
        unityContext = context;

        //Get USB Manager
        usbManager = (UsbManager) context.getSystemService(Context.USB_SERVICE);

        //Create probe table, which is necessary to connect to devices not natively supported by the serial library
        ProbeTable customTable = new ProbeTable();
        customTable.addProduct(vendorID, productID, CdcAcmSerialDriver.class);

        //Create a prober using the probe table, find all matching devices
        UsbSerialProber prober = new UsbSerialProber(customTable);
        List<UsbSerialDriver> drivers = prober.findAllDrivers(usbManager);

        //Check the number of devices found
        if(drivers.isEmpty()) {
            //There is no device plugged in, return false
            return false;
        }

        //Get the first driver from the list (should only be one)
        deviceDriver = drivers.get(0);
        return true;
    }

    //Function called from the C#-side to request permissions for the device
    public static boolean requestUSBPermissions() {
        //Attempt to open a connection
        usbDeviceConnection = usbManager.openDevice(deviceDriver.getDevice());

        //In the case we already have permissions, just return true
        if(usbDeviceConnection != null) {
            return true;
        }

        //Request permissions
        usbManager.requestPermission(deviceDriver.getDevice(), PendingIntent.getBroadcast(unityContext, 0, new Intent("USB Permissions"), 0));

        //Repeatedly check if the connection is null for 10 seconds, otherwise give up and return false
        int attempts = 0;
        while(usbDeviceConnection == null) {
            if(attempts > 10) {
                return false;
            }
            usbDeviceConnection = usbManager.openDevice(deviceDriver.getDevice());
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                unityCallback.onException(e);
            }
            attempts++;
        }
        return true;
    }

    //Function called from C#-side to open the port and register serial library callbacks
    public static boolean openPort(int baudRate) {
        //We can safely assume that the correct port is the first one
        usbSerialPort = deviceDriver.getPorts().get(0);

        //Open the port
        try {
            usbSerialPort.open(usbDeviceConnection);
        } catch (IOException e) {
            unityCallback.onException(e);
            return false;
        }

        //Set the parameters (other than baud, this is very standard)
        try {
            usbSerialPort.setParameters(baudRate, 8, UsbSerialPort.STOPBITS_1, UsbSerialPort.PARITY_NONE);
        } catch (IOException e) {
            unityCallback.onException(e);
            return false;
        }

        //Register serial library callbacks
        SerialInputOutputManager serialInputOutputManager = new SerialInputOutputManager(usbSerialPort, new SerialListener());
        Executors.newSingleThreadExecutor().submit(serialInputOutputManager);
        return true;
    }

    //Inner-class to act as listener for serial library + pass events to C#-side
    private static class SerialListener implements SerialInputOutputManager.Listener{
        @Override
        public void onNewData(byte[] data) {
            unityCallback.onNewData(data);
        }

        @Override
        public void onRunError(Exception e) {
            unityCallback.onException(e);
        }
    }

    //Function called from C#-side to write data to the serial port
    public static boolean write(byte[] data, int timeout) {
        try {
            usbSerialPort.write(data, timeout);
        } catch (IOException e) {
            unityCallback.onException(e);
            return false;
        }
        return true;
    }
}