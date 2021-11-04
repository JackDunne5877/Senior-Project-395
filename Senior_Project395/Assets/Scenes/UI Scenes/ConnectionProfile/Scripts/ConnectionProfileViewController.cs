using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dating_Platform
{
    public class ConnectionProfileViewController : MonoBehaviour
    {
        private User _connectionPlayer;
        public GameObject PlayerImagesContainer;
        public GameObject PlayerImagePrefab;
        public Image ProfileImg;
        public Text ProfileName;
        public Text GenderText;
        public Text AgeText;
        public Text BioText;
        List<Sprite> _playerImages = new List<Sprite>();
        public GameObject FullGallery;

        public List<Sprite> PlayerImages { get => _playerImages; set { _playerImages = value; showPlayerImages(); } }

        public User ConnectionPlayer { 
            get => _connectionPlayer;
            set {
                _connectionPlayer = value;
                populateConnectionInfo();
                    }
        }

        // Start is called before the first frame update
        void Start()
        {
            //connectionPlayer = DatabaseConnection.getConnectionPlayerInfo(SingletonManager.Instance.Player, "12345", SingletonManager.Instance.viewingConnectionPlayerId);
            ConnectionPlayer = SingletonManager.Instance.Player; //TODO switch
            Debug.Log("tried to set connection Player to show info");
            FullGallery.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void populateConnectionInfo()
        {
            for(int i= 0; i < 3; i++) 
            {
                Sprite img = ConnectionPlayer.PlayerImages[i];
                AddPlayerImage(img);
            }

            ProfileImg.sprite = ConnectionPlayer.ProfileImg;
            ProfileName.text = ConnectionPlayer.DisplayName;
            GenderText.text = SingletonManager.GenderOption.GetName(typeof(SingletonManager.GenderOption), ConnectionPlayer.genderIdentity);
            AgeText.text = ConnectionPlayer.age.ToString();
            BioText.text = ConnectionPlayer.bio;
        }

        public void AddPlayerImage(Sprite newSprite)
        {
            PlayerImages.Add(newSprite);
            showPlayerImages();
        }

        void showPlayerImages()
        {
            Debug.Log("showing profile images...");
            foreach (Transform child in PlayerImagesContainer.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < PlayerImages.Count; i++)
            {
                GameObject newPicChild = Instantiate(PlayerImagePrefab);
                Image newImgComponent = newPicChild.transform.GetChild(0).GetComponent<Image>();
                newImgComponent.sprite = PlayerImages[i];
                newImgComponent.preserveAspect = true;
                newPicChild.transform.SetParent(PlayerImagesContainer.transform, false);
            }
        }

        public Image galleryCurrentImg;
        public Text galleryCurrentImgNumTxt;
        private int _galleryCurrentImgNum;
        public int GalleryCurrentImgNum {
            get => _galleryCurrentImgNum;
            set
            {
                if (value == -1) value = PlayerImages.Count - 1;

                value = value % PlayerImages.Count;
                _galleryCurrentImgNum = value;
                galleryCurrentImgNumTxt.text = $"{value+1} / {PlayerImages.Count}";
                galleryCurrentImg.sprite = PlayerImages[value];
                galleryCurrentImg.preserveAspect = true;
            }
        }

        //gallery view

        public void nextImg()
        {
            GalleryCurrentImgNum++;
        }
        public void prevImg()
        {
            GalleryCurrentImgNum--;
        }

        public void showFullGallery()
        {
            GalleryCurrentImgNum = 0;
            FullGallery.SetActive(true);
        }

        public void closeFullGallery()
        {
            FullGallery.SetActive(false);
        }
    }
}