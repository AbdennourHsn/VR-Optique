using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CleanImplementation
{
    public class UiManager : MonoBehaviour
    {
        public MenuOptions twoOptions;
        public MenuOptions treeOptions;
        public MenuOptions foorOptions;
        public MenuOptions fiveOptions;
        public MenuOptions sixOptions;

        private void HideAll()
        {
            twoOptions.gameObject.SetActive(false);
            treeOptions.gameObject.SetActive(false);
            foorOptions.gameObject.SetActive(false);
            fiveOptions.gameObject.SetActive(false);
            sixOptions.gameObject.SetActive(false);
        }

        public void SetUI(List<OptionLoin> options)
        {
            if (options.Count == 2)
            {
                twoOptions.gameObject.SetActive(true);
                twoOptions.SetUpOptions(options);
            }
            else if (options.Count == 3)
            {
                treeOptions.gameObject.SetActive(true);
                treeOptions.SetUpOptions(options);
            }
            else if (options.Count == 4)
            {
                foorOptions.gameObject.SetActive(true);
                foorOptions.SetUpOptions(options);
            }
            else if (options.Count == 5)
            {
                fiveOptions.gameObject.SetActive(true);
                fiveOptions.SetUpOptions(options);
            }
            else if (options.Count == 5)
            {
                sixOptions.gameObject.SetActive(true);
                sixOptions.SetUpOptions(options);
            }
        }
    }
}
