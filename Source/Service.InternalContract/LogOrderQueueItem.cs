﻿using System;


namespace Service.InternalContract
{
    public sealed class LogOrderQueueItem
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}