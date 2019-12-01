using System;

namespace MyTrBox_WebAPI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SecretAttribute : Attribute
    {
    }
}