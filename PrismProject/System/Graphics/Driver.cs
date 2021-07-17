﻿using Cosmos.System.Graphics;
using System;
using System.Drawing;
using System.IO;

namespace PrismProject
{
    internal class Driver
    {
        public static int screenY = 600;
        public static int screenX = 800;
        public static VBECanvas canvas = new VBECanvas(new Mode(screenX, screenY, ColorDepth.ColorDepth32));
        public static string font = "YuGothicUI";
        public static string Clock = "YuGothicUI";
        public static string clock_large = "bigclock";
        private static readonly Random rnd = new Random();
        public static int randomcolor = rnd.Next(256) + rnd.Next(256) + rnd.Next(256);

        public static void Init()
        {
            string CustomCharset = "🡬abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()~`\"\':;?/>.<,{[}]\\|+=_-";
            MemoryStream YuGothicUICustomCharset16 = new MemoryStream(Convert.FromBase64String("AAAAAB/8GAwUFBIkEUQQhBCEEUQSJBQUGBwf/AAAAAAAAAAAH/wYDBQUEiQRRBCEEIQRRBIkFBQYHB/8AAAAAAAAAAAAAAAAAAAOABMAAQAPABEAEwAdAAAAAAAAAAAAAAAAABAAEAAQABcAGYAQgBCAEIAZgBcAAAAAAAAAAAAAAAAAAAAAAAAADwAZABAAEAAQABAADgAAAAAAAAAAAAAAAAAAgACAAIAOgBGAEIAQgBGAEYAOgAAAAAAAAAAAAAAAAAAAAAAAAA4AEQARAB8AEAAQAA8AAAAAAAAAAAAAAAAADAAIABAAPAAQABAAEAAQABAAEAAAAAAAAAAAAAAAAAAAAAAAAAAOgBGAEIAQgBGAEYAOgAEAAQAeAAAAAAAAABAAEAAQABcAGQARABEAEQARABEAAAAAAAAAAAAAAAAAEAAAAAAAEAAQABAAEAAQABAAEAAAAAAAAAAAAAAAAAAQAAAAAAAQABAAEAAQABAAEAAQABAAEABgAAAAAAAAABAAEAAQABMAEgAUABgAFAASABEAAAAAAAAAAAAAAAAAEAAQABAAEAAQABAAEAAQABAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAABZwGZAREBEQERAREBEQAAAAAAAAAAAAAAAAAAAAABcAGQARABEAEQARABEAAAAAAAAAAAAAAAAAAAAAAAAADwARgBCAEIAQgBGADwAAAAAAAAAAAAAAAAAAAAAAAAAXABmAEIAQgBCAGYAXABAAEAAQAAAAAAAAAAAAAAAAAA6AEYAQgBCAEYARgA6AAIAAgACAAAAAAAAAAAAAAAAAFAAYABAAEAAQABAAEAAAAAAAAAAAAAAAAAAAAAAAAAAeABAAEAAMAAIAAgAcAAAAAAAAAAAAAAAAAAAAEAAQADwAEAAQABAAEAAYAAwAAAAAAAAAAAAAAAAAAAAAAAAAEQARABEAEQARABEADwAAAAAAAAAAAAAAAAAAAAAAAAAhABEAEgASAAoADAAMAAAAAAAAAAAAAAAAAAAAAAAAACIgEyATIBVAFUAMwAiAAAAAAAAAAAAAAAAAAAAAAAAAEgASAAwADAAMABIAMgAAAAAAAAAAAAAAAAAAAAAAAAAhABEAEgASAAoADAAMAAgACAAwAAAAAAAAAAAAAAAAAB4AAgAEAAgACAAQAD8AAAAAAAAAAAAAAAAAAAAGAAYABQAJAAkAGIAfgBDAMEAAAAAAAAAAAAAAAAAAAB8AEQARABEAHgARgBCAEYAfAAAAAAAAAAAAAAAAAAAAB4AIABAAEAAQABAAEAAIAAeAAAAAAAAAAAAAAAAAAAAfABDAEEAQQBBgEEAQQBCAHwAAAAAAAAAAAAAAAAAAAB8AEAAQABAAHwAQABAAEAAfAAAAAAAAAAAAAAAAAAAAHwAQABAAEAAfABAAEAAQABAAAAAAAAAAAAAAAAAAAAAHwAhAEAAQABHAEEAQQAhAB4AAAAAAAAAAAAAAAAAAABBAEEAQQBBAH8AQQBBAEEAQQAAAAAAAAAAAAAAAAAAAEAAQABAAEAAQABAAEAAQABAAAAAAAAAAAAAAAAAAAAAMAAwADAAMAAwADAAIAAgAMAAAAAAAAAAAAAAAAAAAABGAEQASABQAHAAUABIAEQAQgAAAAAAAAAAAAAAAAAAAEAAQABAAEAAQABAAEAAQAB8AAAAAAAAAAAAAAAAAAAAAABgQGDAUMBQwElASUBKQEZAREAAAAAAAAAAAAAAAABhgHGAUYBJgEmARYBDgEKAQYAAAAAAAAAAAAAAAAAAAB4AIQBAgECAQIBAgECAIQAeAAAAAAAAAAAAAAAAAAAAfABGAEIAQgBEAHgAQABAAEAAAAAAAAAAAAAAAAAAAAAeACEAQIBAgECAQIBAgCEAHwABgAAAAAAAAAAAAAAAAHwARgBCAEQAeABMAEQAQgBCAAAAAAAAAAAAAAAAAAAAPABEAEAAYAA4AAwABAAEAHgAAAAAAAAAAAAAAAAAAAD+ABAAEAAQABAAEAAQABAAEAAAAAAAAAAAAAAAAAAAAEEAQQBBAEEAQQBBAEEAYgA8AAAAAAAAAAAAAAAAAAAAgQBCAEIAYgAkACQAHAAYABgAAAAAAAAAAAAAAAAAAAAAAIIQRjBGIEUgaSApQClAMMAQwAAAAAAAAAAAAAAAAEIAZAAkABgAGAAYACQARgBCAAAAAAAAAAAAAAAAAAAAwgBEAEQAKAAoABAAEAAQABAAAAAAAAAAAAAAAAAAAAB+AAQADAAIABAAMAAgAEAA/gAAAAAAAAAAAAAAAAAAABgAaAAIAAgACAAIAAgACAAIAAAAAAAAAAAAAAAAAAAAOABEAAQABAAIABAAYABAAHwAAAAAAAAAAAAAAAAAAAB4AEwABAAIADAADAAEAAQAeAAAAAAAAAAAAAAAAAAAAAgAGAAYACgASABIAP4ACAAIAAAAAAAAAAAAAAAAAAAAfABAAEAAeAAMAAQABABMAHgAAAAAAAAAAAAAAAAAAAAcACAAQAB4AEQARABEAEQAOAAAAAAAAAAAAAAAAAAAAHwABAAIAAgAEAAQABAAIAAgAAAAAAAAAAAAAAAAAAAAOABEAEQARAA4AEQARABEADgAAAAAAAAAAAAAAAAAAAA4AEQARABEAEQAPAAEAAwAeAAAAAAAAAAAAAAAAAAAADgARABEAEQARABEAEQARAA4AAAAAAAAAAAAAAAAAAAAQABAAEAAQABAAEAAAABAAEAAAAAAAAAAAAAAAAAAAAAAAA+AEEAmIEkgSSBJIEkgLsAwAA+AAAAAAAAAAAAAABQAFAB+ACQAKAD+ACgASAAAAAAAAAAAAAAAAAAAABAAPABUAFAAcAA4ABwAFABUAHgAEAAAAAAAAAAAAAAAAABxAEkASgBMAHWACkASQBJAIYAAAAAAAAAAAAAAAAAAAAgAGAAUACIAIgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHAAkACSAOIAsgEKAQwBDADzAAAAAAAAAAAAAAAAAAAAgAHgAIABQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAIABAAEAAQABAAEAAQABAACAAIAAAAAAAAAAAAAAAgABAACAAIAAgACAAIAAgACAAQABAAAAAAAAAAAAAAAAAAAAAAAAAADkATgAAAAAAAAAAAAAAAAAAAAAAAABAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUABQAFAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAEAAAAAAAAAAQABAAAAAAAAAAAAAAAAAAAAAAAAAAEAAQAAAAAAAAAAAAEAAQACAAAAAAAAAAAAAAAB4AAgACAAQACAAIAAAACAAIAAAAAAAAAAAAAAAAAAAAAgACAAQABAAIAAgACAAQABAAIAAgAAAAAAAAAAAAAAAAAAAACAAGAAEAAYAGAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAQAAAAAAAAAAAAAAAAAAAAAAAAAACAAwAEAAwAAwAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAQACAAAAAAAAAAAAAAAAwACAAIAAgAEAAQABAACAAIAAgADAAAAAAAAAAAAAAAHAAQABAAEAAQABAAEAAQABAAEAAcAAAAAAAAAAAAAAAwABAAEAAQABgACAAYABAAEAAQADAAAAAAAAAAAAAAADgACAAIAAgACAAIAAgACAAIAAgAOAAAAAAAAAAAAAAAEQARAAoACgAfAAQAHwAEAAQAAAAAAAAAAAAAAAAAEAAQABAAEAAQABAAEAAQABAAEAAQABAAEAAAAAAAAAAAAAAAAgACAAIAH4ACAAIAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH4AAAAAAH4AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD4AAAAAAAAAAAAAAAAAAAAAAAAAHAAAAAAAAAAAAAAAAAAAAAAA"));
            BitFont.RegisterBitFont("YuGothicUI", new BitFontDescriptor(CustomCharset, YuGothicUICustomCharset16, 16));
            string clock = "1234567890:apm";
            MemoryStream MSReferenceSansSerifCustomCharset16 = new MemoryStream(Convert.FromBase64String("AAAAAAAAAAAAAAQAHAAEAAQABAAEAAQAHwAAAAAAAAAAAAAAAAAAAAAADgARAAEAAgAEAAgAEAAfAAAAAAAAAAAAAAAAAAAAAAAOABEAAQAGAAEAAQARAA4AAAAAAAAAAAAAAAAAAAAAAAIABgAKABIAIgA/AAIAAgAAAAAAAAAAAAAAAAAAAAAAHwAQABAAHgABAAEAEQAOAAAAAAAAAAAAAAAAAAAAAAAGAAgAEAAeABEAEQARAA4AAAAAAAAAAAAAAAAAAAAAAB8AAQACAAIABAAEAAgACAAAAAAAAAAAAAAAAAAAAAAADgARABEADgARABEAEQAOAAAAAAAAAAAAAAAAAAAAAAAOABEAEQARAA8AAQACAAwAAAAAAAAAAAAAAAAAAAAAAA4AEQARABEAEQARABEADgAAAAAAAAAAAAAAAAAAAAAAAAAAAAgACAAAAAAACAAIAAAAAAAAAAAAAAAAAAAAAAAAAAAADgABAA8AEQARAA8AAAAAAAAAAAAAAAAAAAAAAAAAAAAeABEAEQARABEAHgAQABAAAAAAAAAAAAAAAAAAAAAAAB7gERAREBEQERAREAAAAAAAAA=="));
            BitFont.RegisterBitFont("bigclock", new BitFontDescriptor(clock, MSReferenceSansSerifCustomCharset16, 16));
        }

        public static void Clear(Color clr)
        {
            canvas.Clear(clr);
        }
    }
}