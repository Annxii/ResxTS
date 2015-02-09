// Guids.cs
// MUST match guids.h
using System;

namespace ResxTS.VSExtensions.CustomTool
{
    static class GuidList
    {
        public const string guidPkgString = "f757200b-84f4-4b59-a445-715ed62b1818";
        public const string guidCmdSetString = "d78c259c-6239-4ab1-b203-26e45b7e43ad";
        public const string guidCustomToolString = "B31AB660-A455-4CCB-A668-56DD92D0ACBA";

        public static readonly Guid guidCmdSet = new Guid(guidCmdSetString);
    };
}