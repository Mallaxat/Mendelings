using CommunityToolkit.Mvvm.ComponentModel;
using Mendelings.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core
{
    public partial class PetUnlockCollection : ObservableObject
    {
        public string BodyGene => BodyDominant ? "B_" : "bb";
        public string HeadGene => HeadDominant ? "H_" : "hh";
        public string EarsGene => EarsDominant ? "A_" : "aa";
        public string EyesGene => EyesDominant ? "E_" : "ee";
        public string HornsGene => HornsDominant ? "G_" : "gg";
        public string TailGene => TailDominant ? "T_" : "tt";
        
        [ObservableProperty]
        private bool bodyDominant;

        [ObservableProperty]
        private bool headDominant;

        [ObservableProperty]
        private bool earsDominant;

        [ObservableProperty]
        private bool eyesDominant;

        [ObservableProperty]
        private bool hornsDominant;

        [ObservableProperty]
        private bool tailDominant;

        [ObservableProperty]
        private bool isUnlocked;

        [ObservableProperty]
        private AppearancePet? appearancePet;



    }
}
