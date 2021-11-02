using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using SimpleFileBrowser;

namespace Dating_Platform
{
    public class UserProfileInfoForm : MonoBehaviour
    {
        private bool _hasChanges = false;

        public InputField bioTextField;
        public InputField ageTextField;
        public Dropdown GenderIdentityDrop;
        public GameObject GenderPreferencesGrid;
        List<Toggle> GenderPreferencesToggles = new List<Toggle>();
        public GameObject picsCollection;
        public GameObject ImagePrefab;
        public GameObject GenderTogglePrefab;
        public Button SaveBtn;

        string bioText;
        int age;
        GenderOption genderIdentity;
        List<GenderOption> genderPreferences = new List<GenderOption>();
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
            foreach (int gdrOpIndex in GenderOption.GetValues(typeof(GenderOption)))
            {
                string gdrOp = GenderOption.GetName(typeof(GenderOption), gdrOpIndex);
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
            Player myPlayer = SingletonManager.Instance.Player;
            fillingFromDatabase = true;
            bioText = myPlayer.bio;
            bioTextField.text = myPlayer.bio;
            age = myPlayer.age;
            ageTextField.text = myPlayer.age.ToString();
            genderIdentity = myPlayer.genderIdentity;
            GenderIdentityDrop.value = (int)genderIdentity;

            foreach (GenderOption go in myPlayer.genderPreferences)
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

            genderIdentity = (GenderOption)newGdrIdIndex;
            Debug.Log("gender identity set: " + genderIdentity);
        }

        public void updateGenderPreferences()
        {
            if (!fillingFromDatabase)
                UnsavedChanges = true;

            genderPreferences = new List<GenderOption>();
            for (int i = 0; i < GenderPreferencesToggles.Count; i++)
            {
                Toggle genderPref = GenderPreferencesToggles[i];
                if (genderPref.isOn)
                {
                    genderPreferences.Add((GenderOption)i);
                    Debug.Log("gender prefs:");
                    foreach (GenderOption pref in genderPreferences)
                    {
                        Debug.Log(GenderOption.GetName(typeof(GenderOption), pref));
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
        public void AddGenderPreference(GenderOption newGenOpt)
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
                int copy = i; //thanks Jon Skeet
                newPicChild.GetComponentInChildren<Button>().onClick.AddListener(delegate { deleteImg(copy); });
            }
        }

        void deleteImg(int imgToDelete)
        {
            Debug.Log("image to delete: " + imgToDelete);
            Debug.Log("profileImagesSize: " + ProfileImages.Count);
            ProfileImages.RemoveAt(imgToDelete);
            UnsavedChanges = true;
        }

        public void saveChanges()
        {
            //TODO send cahnges to server
            UnsavedChanges = false;

        }

        public void cleanUpForViewSwitch()
        {
            Debug.Log("cleaning up...");
            SaveBtn.gameObject.SetActive(false);
            //cleanedup = true;
        }

        public void reloadForViewSwitch()
        {
            Debug.Log("reloading...");
            SaveBtn.gameObject.SetActive(UnsavedChanges);
            //cleanedup = false;
        }

    }
}