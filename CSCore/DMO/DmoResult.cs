﻿namespace CSCore.DMO
{
    //mediaerr.h
    public enum DmoResult
    {
        S_OK = 0x0,
        DMO_E_INVALIDSTREAMINDEX    = unchecked((int)0x80040201),
        DMO_E_INVALIDTYPE           = unchecked((int)0x80040202),
        DMO_E_TYPE_NOT_SET          = unchecked((int)0x80040203),
        DMO_E_NOTACCEPTING          = unchecked((int)0x80040204),
        DMO_E_TYPE_NOT_ACCEPTED     = unchecked((int)0x80040205),
        DMO_E_NO_MORE_ITEMS         = unchecked((int)0x80040206)
    }
}