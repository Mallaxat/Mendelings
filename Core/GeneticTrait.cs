using System;
using System.Collections.Generic;
using System.Text;

namespace Mendelings.Core
{
    //Отвечает за описание признака
    public class GeneticTrait
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        //Название аллели
        public char DominantAllele { get; set; }
        public char RecessiveAllele { get; set; }
        //Описание гена
        public string DominantPhenotype { get; set; } = string.Empty;
        public string RecessivePhenotype { get; set; } = string.Empty;
        //Путь к изображению
        public string DominantAssetPath { get; set; } = string.Empty;
        public string RecessiveAssetPath { get; set; } = string.Empty;

    }
}
