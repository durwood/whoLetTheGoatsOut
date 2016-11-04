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
        CheckmarkInCircle,
        Checkmark,
        CheckmarkInBox,
        EmptyBox,
        Question1,
        Fence1,
        Fence2,
        Fence3,
        ChainLinkFence1,
        ChainLinkFence2,
        ChainLinkFence3,
        ChainLinkFence4,
        FlashBang,
        Question2,
        ChainLinkSlatFence1,
        ChainLinkSlatFence2,
        ChainLinkSlatFence3,
        ChainLinkSlatFence4,
        ChainLinkSlatFence5
    }

    public class BoardIcons
    {
        private readonly Dictionary<BoardIcon, string> _boardIconMap = new Dictionary<BoardIcon, string>
        {
            {BoardIcon.MarkGoat, "goat-icon.png"},
            {BoardIcon.One, "numbers-1-icon.png"},
            {BoardIcon.Two, "numbers-2-icon.png"},
            {BoardIcon.Three, "numbers-3-icon.png"},
            {BoardIcon.Four, "numbers-4-icon.png"},
            {BoardIcon.Five, "numbers-5-icon.png"},
            {BoardIcon.Six, "numbers-6-icon.png"},
            {BoardIcon.Seven, "numbers-7-icon.png"},
            {BoardIcon.Eight, "numbers-8-icon.png"},
            {BoardIcon.CheckmarkInCircle, "checkbox-in-circle-icon.png"},
            {BoardIcon.Checkmark, "checkmark-icon.png"},
            {BoardIcon.CheckmarkInBox, "checkmark-in-box-icon.png"},
            {BoardIcon.EmptyBox, "empty-box-icon.png"},
            {BoardIcon.Question1, "faq-icon.png"},
            {BoardIcon.Fence1, "fence-1-icon.png"},
            {BoardIcon.Fence2, "fence-2-icon.png"},
            {BoardIcon.Fence3, "fence-3-icon.png"},
            {BoardIcon.ChainLinkFence1, "fence-chainlink-1-icon.png"},
            {BoardIcon.ChainLinkFence2, "fence-chainlink-2-icon.png"},
            {BoardIcon.ChainLinkFence3, "fence-chainlink-3-icon.jpg"},
            {BoardIcon.ChainLinkFence4, "fence-chainlink-4-icon.jpg"},
            {BoardIcon.FlashBang, "flash-bang-icon.png"},
            {BoardIcon.Question2, "question-icon.png"},
            {BoardIcon.ChainLinkSlatFence1, "chainlink-slat-1-icon.jpg"},
            {BoardIcon.ChainLinkSlatFence2, "chainlink-slat-2-icon.jpg"},
            {BoardIcon.ChainLinkSlatFence3, "chainlink-slat-3-icon.jpg"},
            {BoardIcon.ChainLinkSlatFence4, "chainlink-slat-4-icon.jpg"},
            {BoardIcon.ChainLinkSlatFence5, "chainlink-slat-5-icon.jpg"}
        };

        public string GetResourceName(BoardIcon icon)
        {
            if (_boardIconMap.ContainsKey(icon))
                return _boardIconMap[icon];
            throw new ArgumentException($"Icon {icon} not found in BoardIcons resource mapping table");
        }
    }
}