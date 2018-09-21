using System;
using System.Collections.Generic;
using System.Text;

namespace RxNLizer.Model
{
    public class RxClassDrugInfo
    {
        public MinConcept minConcept { get; set; }
        public RxClassMinconceptItem rxclassMinConceptItem { get; set; }
        public string rela { get; set; }
        public string relaSource { get; set; }

    }
}
