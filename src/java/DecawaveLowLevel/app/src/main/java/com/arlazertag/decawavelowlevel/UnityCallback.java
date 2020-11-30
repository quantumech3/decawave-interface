package com.arlazertag.decawavelowlevel;

public interface UnityCallback {
    public void onException(Exception e);
    public void onNewData(byte[] data);
}
