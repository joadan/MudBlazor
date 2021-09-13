﻿using System;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using MudBlazor.Extensions;
using MudBlazor.Utilities;

namespace MudBlazor
{
    public partial class MudTooltip : MudComponentBase
    {
        protected string ContainerClass => new CssBuilder("mud-tooltip-root")
            .AddClass("mud-tooltip-inline", Inline)
            .Build();
        protected string Classname => new CssBuilder("mud-tooltip")
            .AddClass($"mud-tooltip-default", Color == Color.Default)
            .AddClass($"mud-theme-{Color.ToDescriptionString()}", Color != Color.Default)
            .AddClass(Class)
            .Build();


        [CascadingParameter]
        public bool RightToLeft { get; set; }

        /// <summary>
        /// The color of the component. It supports the theme colors.
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// Sets the text to be displayed inside the tooltip.
        /// </summary>
        [Parameter] public string Text { get; set; }

        /// <summary>
        /// Changes the default transition delay in milliseconds.
        /// </summary>
        [Parameter] public double Delay { get; set; } = 200;

        /// <summary>
        /// Changes the default transition delay in seconds.
        /// </summary>
        [Obsolete]
        [Parameter]
        public double Delayed
        {
            get { return Delay / 1000; }
            set { Delay = value * 1000; }
        }

        /// <summary>
        /// Tooltip placement.
        /// </summary>
        [Parameter] public Placement Placement { get; set; } = Placement.Bottom;

        /// <summary>
        /// Child content of component.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Tooltip content. May contain any valid html
        /// </summary>
        [Parameter] public RenderFragment TooltipContent { get; set; }

        /// <summary>
        /// Determines if this component should be inline with it's surrounding (default) or if it should behave like a block element.
        /// </summary>
        [Parameter] public Boolean Inline { get; set; } = true;

        private bool _isVisible;
        public void HandleMouseOver() => _isVisible = true;
        //private void HandleMouseOut() => _isVisible = false;

        private void HandleMouseOut() => _isVisible = true;

        private Direction Direction { get; set; } = Direction.Bottom;

        protected string GetTimeDelay()
        {
            return $"transition-delay: {Delay.ToString(CultureInfo.InvariantCulture)}ms;{Style}";
        }

        private Direction ConvertPlacement()
        {
            return Placement switch
            {
                Placement.Left => RightToLeft ? Direction.End : Direction.Start,
                Placement.Right => RightToLeft ? Direction.Start : Direction.End,
                Placement.Top => Direction.Top,
                Placement.Bottom => Direction.Bottom,
                _ => Direction
            };
        }
    }
}
