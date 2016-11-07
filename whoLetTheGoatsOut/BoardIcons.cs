using System;
using System.Collections.Generic;

namespace bombsweeperWinform
{
    public enum BoardIcon
    {
        MarkGoat,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        BlockingFence
    }

    public class BoardIcons
    {
        private readonly Dictionary<BoardIcon, string> _boardIconMap = new Dictionary<BoardIcon, string>
        {
            {BoardIcon.MarkGoat, "goat-over-fence-icon.png"},
            {BoardIcon.One, "numbers-1-icon.png"},
            {BoardIcon.Two, "numbers-2-icon.png"},
            {BoardIcon.Three, "numbers-3-icon.png"},
            {BoardIcon.Four, "numbers-4-icon.png"},
            {BoardIcon.Five, "numbers-5-icon.png"},
            {BoardIcon.Six, "numbers-6-icon.png"},
            {BoardIcon.Seven, "numbers-7-icon.png"},
            {BoardIcon.Eight, "numbers-8-icon.png"},
            {BoardIcon.BlockingFence, "fence-chainlink-1-icon.png"}
        };

        public string GetResourceName(BoardIcon icon)
        {
            if (_boardIconMap.ContainsKey(icon))
                return _boardIconMap[icon];
            throw new ArgumentException($"Icon {icon} not found in BoardIcons resource mapping table");
        }
    }
}