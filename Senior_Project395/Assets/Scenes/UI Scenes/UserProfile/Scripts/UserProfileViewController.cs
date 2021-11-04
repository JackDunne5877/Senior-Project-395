using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SimpleFileBrowser;

namespace Dating_Platform
{
    public class UserProfileViewController : MonoBehaviour
    {
        private bool _hasChanges = false;

        public Image profileImage;
        public InputField bioTextField;
        public InputField ageTextField;
        public Dropdown GenderIdentityDrop;
        public GameObject GenderPreferencesGrid;
        List<Toggle> GenderPreferencesToggles = new List<Toggle>();
        public GameObject picsCollection;
        public GameObject ImagePrefab;
        public GameObject GenderTogglePrefab;
        public Button SaveBtn;
        public Text DisplayNameTxt;

        string bioText;
        int age;
        SingletonManager.GenderOption genderIdentity;
        List<SingletonManager.GenderOption> genderPreferences = new List<SingletonManager.GenderOption>();
        List<Sprite> _profileImages = new List<Sprite>();

        bool fillingFromDatabase = false;
        bool populatedPlayerInfo = false;
        //bool cleanedup = false;

        bool UnsavedChanges
        {
            get { return _hasChanges; }
            set
            {
                _hasChanges = value;
                SaveBtn.gameObject.SetActive(_hasChanges);
            }
        }

        public List<Sprite> ProfileImages { get => _profileImages; set{ _profileImages = value; showProfileImages(); } }

        // Start is called before the first frame update
        void Start()
        {
            //initialize all options based on Player definition
            foreach (int gdrOpIndex in SingletonManager.GenderOption.GetValues(typeof(SingletonManager.GenderOption)))
            {
                string gdrOp = SingletonManager.GenderOption.GetName(typeof(SingletonManager.GenderOption), gdrOpIndex);
                GenderIdentityDrop.AddOptions(new List<Dropdown.OptionData>() { new Dropdown.OptionData(gdrOp) });
                GameObject gdrOpToggle = GameObject.Instantiate(GenderTogglePrefab);
                gdrOpToggle.GetComponentInChildren<Text>().text = gdrOp;
                gdrOpToggle.transform.SetParent(GenderPreferencesGrid.transform);
                gdrOpToggle.transform.localScale = Vector3.one;
                gdrOpToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate { updateGenderPreferences(); });
                GenderPreferencesToggles.Add(gdrOpToggle.gameObject.GetComponent<Toggle>());
            }

            if (SingletonManager.Instance != null && SingletonManager.Instance.Player != null)
            {
                if (!populatedPlayerInfo)
                {
                    Debug.Log("populating Player info...");
                    populatePlayerInfo();
                }
                else
                {
                    Debug.Log("already populated Player info.");
                }
            }
            else
            {
                Debug.LogWarning("on UserProfile view, but player is null");
            }
        }

        void populatePlayerInfo()
        {
            populatedPlayerInfo = true;
            User myPlayer = SingletonManager.Instance.Player;
            fillingFromDatabase = true;
            bioText = myPlayer.bio;
            bioTextField.text = myPlayer.bio;
            age = myPlayer.age;
            ageTextField.text = myPlayer.age.ToString();
            genderIdentity = myPlayer.genderIdentity;
            GenderIdentityDrop.value = (int)genderIdentity;
            profileImage.sprite = myPlayer.ProfileImg;
            DisplayNameTxt.text = myPlayer.DisplayName;

            foreach (SingletonManager.GenderOption go in myPlayer.genderPreferences)
            {
                AddGenderPreference(go);
            }

            foreach (Sprite sp in myPlayer.PlayerImages)
            {
                AddProfileImage(sp);
            }
            

            fillingFromDatabase = false;
            UnsavedChanges = false;
        }

        // Update is called once per frame
        void Update()
        {
            //if (!populatedPlayerInfo && SingletonManager.Instance.player != null)
            //{
            //    populatePlayerInfo();
            //}
        }

        public void updateBio(string newBio)
        {
            if (!fillingFromDatabase)
                UnsavedChanges = true;
            bioText = newBio;
        }

        public void updateAge(string newAge)
        {

            if (System.Int32.TryParse(newAge, out int ageResult))
            {
                age = ageResult;

                if (!fillingFromDatabase)
                    UnsavedChanges = true;
            }
        }

        public void updateGenderIdentity(int newGdrIdIndex)
        {
            if (!fillingFromDatabase)
                UnsavedChanges = true;

            genderIdentity = (SingletonManager.GenderOption)newGdrIdIndex;
            Debug.Log("gender identity set: " + genderIdentity);
        }

        public void updateGenderPreferences()
        {
            if (!fillingFromDatabase)
                UnsavedChanges = true;

            genderPreferences = new List<SingletonManager.GenderOption>();
            for (int i = 0; i < GenderPreferencesToggles.Count; i++)
            {
                Toggle genderPref = GenderPreferencesToggles[i];
                if (genderPref.isOn)
                {
                    genderPreferences.Add((SingletonManager.GenderOption)i);
                    Debug.Log("gender prefs:");
                    foreach (SingletonManager.GenderOption pref in genderPreferences)
                    {
                        Debug.Log(SingletonManager.GenderOption.GetName(typeof(SingletonManager.GenderOption), pref));
                    }
                }
            }
        }

    IEnumerator UploadImageCoroutine()
        {
            FileBrowser.SetFilters(false, new[] { ".jpg", ".jpeg", ".png" });
            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load Image to Profile", "Upload");
            //FileBrowser.WaitForLoadDialog(FileBrowser.OnSuccess, FileBrowser.OnCancel, FileBrowser.PickMode.Files, false, null, null, "Load Image to Profile", "Upload");
            if (FileBrowser.Success)
            {
                string[] paths = FileBrowser.Result;
                Debug.Log("path:" + paths[0]);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(FileBrowserHelpers.ReadBytesFromFile(paths[0]));
                Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                AddProfileImage(newSprite);

                if (!fillingFromDatabase)
                    UnsavedChanges = true;
            }
        }

        public void AddProfileImage(Sprite newSprite)
        {
            ProfileImages.Add(newSprite);
            showProfileImages();
        }
        public void RemoveProfileImage(int imgIndex)
        {
            if (imgIndex < ProfileImages.Count)
            {
                ProfileImages.RemoveAt(imgIndex);
                showProfileImages();
                UnsavedChanges = true;
            }
        }
        public void AddGenderPreference(SingletonManager.GenderOption newGenOpt)
        {
            genderPreferences.Add(newGenOpt);
            GenderPreferencesToggles[(int)newGenOpt].isOn = true;
        }

        public void uploadPicture()
        {
            StartCoroutine(UploadImageCoroutine());
        }

        void showProfileImages()
        {
            Debug.Log("showing profile images...");
            foreach (Transform child in picsCollection.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < ProfileImages.Count; i++)
            {
                GameObject newPicChild = Instantiate(ImagePrefab);
                Image newImgComponent = newPicChild.transform.GetChild(0).GetComponent<Image>();
                newImgComponent.sprite = ProfileImages[i];
                newImgComponent.preserveAspect = true;
                newPicChild.transform.SetParent(picsCollection.transform);
                newPicChild.transform.localScale = Vector3.one;
                Debug.Log("adding delegate to delete: " + i);
                int copyIndex = i; //thanks Jon Skeet
                newPicChild.GetComponentInChildren<Button>().onClick.AddListener(delegate { RemoveProfileImage(copyIndex); });
            }
        }

        /*
        //janky trash system but whatever
        private GenderPrefsCombos convertListToGenderPrefs(List<GenderOption> lis) {
            bool male = false;
            bool female = false;
            bool non = false;

            foreach (var g in lis) {
                if (g == GenderOption.Male) {
                    male = true;
                }
                else if (g == GenderOption.Female)
                {
                    female = true;
                }
                else if (g == GenderOption.NonBinary)
                {
                    non = true;
                }
            }

            if (male && female && non) {
                return GenderPrefsCombos.MFN;
            }
            if (male && female)
            {
                return GenderPrefsCombos.MF;
            }

            if (male && non)
            {
                return GenderPrefsCombos.MN;
            }

            if (female && non)
            {
                return GenderPrefsCombos.FN;
            }

            if (male)
            {
                return GenderPrefsCombos.M;
            }
            else if (female)
            {
                return GenderPrefsCombos.F;
            }
            else if(non)
            {
                return GenderPrefsCombos.N;
            }
            //default case is return they have no preference
            return GenderPrefsCombos.MFN;
        }*/

        public void saveChanges()
        {
            //Potential things to save:
            /*
            string bioText;
            int age;
            GenderOption genderIdentity;
            List<GenderOption> genderPreferences = new List<GenderOption>();
            List<Sprite> _profileImages = new List<Sprite>();
            */

            //TODO send changes to server
            UnsavedChanges = false;

            //For now save to PlayerPrefs on player's disk
            genderPreferences.Add(SingletonManager.GenderOption.NonBinary);
            PlayerPrefs.SetInt(SingletonManager.PROFILE_CONST_HOST_GENDER, (int)genderIdentity);
            //TODO change this to be actual gender the player wants, based on UI
            PlayerPrefs.SetInt(SingletonManager.PROFILE_CONST_GENDER_PREF, (int)genderIdentity);
            PlayerPrefs.Save();

            //List<GenderOption> gIdentity = new List<GenderOption>();
            //gIdentity.Add(genderIdentity);
            //save players gender
            //PlayerPrefs.SetInt(SingletonManager.PROFILE_CONST_HOST_GENDER, (int)convertListToGenderPrefs(gIdentity));
            //PlayerPrefs.SetInt(SingletonManager.PROFILE_CONST_GENDER_PREF, (int)convertListToGenderPrefs(genderPreferences));
        }

        public void cleanUpForViewSwitch()
        {
            Debug.Log("cleaning up...");
            SaveBtn.gameObject.SetActive(false);
        }

        public void reloadForViewSwitch()
        {
            Debug.Log("reloading...");
            SaveBtn.gameObject.SetActive(UnsavedChanges);
        }

    }
}