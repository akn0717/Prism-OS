﻿using Mouse = Cosmos.System.MouseManager;
using System.Collections.Generic;
using PrismOS.Libraries.Graphics;

namespace PrismOS.Libraries.UI
{
    public class Window
    {
        // Window Manager Variables
        public static List<Window> Windows { get; set; } = new();
        public static bool Dragging { get; set; } = false;

        // Class Variables
        public List<Control> Elements { get; set; } = new();
        public FrameBuffer FrameBuffer { get; set; }
        public bool TitleVisible { get; set; } = true;
        public bool Draggable { get; set; } = true;
        public bool Visible { get; set; } = true;
        public bool Pressed { get; set; } = false;
        public bool Hover { get; set; } = false;
        public bool Moving { get; set; } = false;
        public string Text { get; set; } = "";
        public Theme Theme { get; set; }
        public int IX { get; set; }
        public int IY { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public int Height
        {
            get
            {
                return (int)FrameBuffer.Height;
            }
            set
            {
                FrameBuffer = new((uint)Width, (uint)value);
            }
        }
        public int Width
        {
            get
            {
                return (int)FrameBuffer.Width;
            }
            set
            {
                FrameBuffer = new((uint)value, (uint)Height);
            }
        }

        public void Update(FrameBuffer Canvas)
        {
            if (Draggable)
            {
                if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                {
                    if (Mouse.X > X && Mouse.X < X + Width && Mouse.Y > Y && Mouse.Y < Y + 20 && !Moving && !Dragging)
                    {
                        Dragging = true;
                        Windows.Remove(this);
                        Windows.Insert(Windows.Count, this);
                        Moving = true;
                        IX = (int)Mouse.X - X;
                        IY = (int)Mouse.Y - Y;
                    }
                }
                else
                {
                    Dragging = false;
                    Moving = false;
                }
                if (Moving)
                {
                    X = (int)Mouse.X - IX;
                    Y = (int)Mouse.Y - IY;
                }
            }

            if (Visible)
            {
                FrameBuffer.DrawFilledRectangle(0, 0, Width, Height, Theme.Radius, Theme.Background);

                if (TitleVisible)
                {
                    FrameBuffer.DrawFilledRectangle(0, 0, Width, 20, Theme.Radius, Theme.Accent);
                    FrameBuffer.DrawString(Width / 2, 10, Text, Theme.Font, Theme.Foreground, 2, true);
                    FrameBuffer.DrawRectangle(0, 0, Width - 1, Height - 1, Theme.Radius, Theme.Foreground);
                }

                foreach (Control C in Elements)
                {
                    C.OnUpdate.Invoke();

                    if (Mouse.X > X + C.X && Mouse.X <  X + C.X + C.Width && Mouse.Y > Y + C.Y && Mouse.Y < Y + C.Y + C.Height && this == Windows[^1])
                    {
                        C.Hover = true;
                        if (Mouse.MouseState == Cosmos.System.MouseState.Left)
                        {
                            C.Pressed = true;
                        }
                        else
                        {
                            if (C.Pressed)
                            {
                                C.Pressed = false;
                                C.OnClick();
                            }
                        }
                    }
                    else
                    {
                        C.Hover = false;
                    }

                    C.Update(this);
                }

                Canvas.DrawImage(X, Y, FrameBuffer, false);
            }
        }
    }
}