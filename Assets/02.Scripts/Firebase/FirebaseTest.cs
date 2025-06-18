using Firebase.Extensions;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using System;
public class FirebaseTest : MonoBehaviour
{
    private Firebase.FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                Debug.Log("Firebase is ready to use.");
                _app = Firebase.FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                AddRanking();
                GetRankings();
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                Debug.LogError("Firebase is not ready to use. Please check your configuration.");
                // Firebase Unity SDK is not safe to use here.
            }
        });
        
    }

    private void Register()
    {
        string email = "youngsuk0311@gmail.com";
        string password = "BBTibber1!";
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);;
        });
    }

    private void Login()
    {
        string email = "youngsuk0311@gmail.com";
        string password = "BBTibber1!";

        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            // Firebase user has been signed in.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    private void NicknameChange()
    {
        var user = _auth.CurrentUser;
        if (user == null)
            return;
        var profile = new UserProfile
        {
            DisplayName = "Tibbers"
        };

        user.UpdateUserProfileAsync(profile).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("UpdateUserProfileAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                return;
            }
            // User profile has been updated.
            Debug.LogFormat("User profile updated successfully: {0} ({1})",
                user.DisplayName, user.UserId);
        });
    }

    private void GetProfile()
    {
        Firebase.Auth.FirebaseUser user = _auth.CurrentUser;
        if (user != null)
        {
            string name = user.DisplayName;
            string email = user.Email;
            
            System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = user.UserId;
        }
    }

    private void AddRanking()
    {
        Ranking ranking = new Ranking("huhuhu@gmail.com", "허허허", 1023); 
        Dictionary<string, object> rankingDict = new Dictionary<string, object>
        {
            {"Email", ranking.Email },
            {"Nickname", ranking.Nickname },
            {"Score", ranking.Score },
        };
        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDict).ContinueWithOnMainThread(task => {
            //DocumentReference addedDocRef = task.Result;
            Debug.Log(String.Format("Added document with ID: {0}.",task.Id));
        });
    }

    private void GetMyRanking()
    {
        var email = "huhuhu@gmail.com";

        var docRef = _db.Collection("rankings").Document(email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;
            if(snapshot.Exists)
            {
                var rankingDict = snapshot.ToDictionary();
                foreach ( var pair in rankingDict)
                {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }
            }
            else
            {
                Debug.Log("No such document!");
            }
        });
    }

    private void GetRankings()
    {
        Query allrankingsQuery = _db.Collection("rankings");
        allrankingsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            if (snapshot.Count == 0)
            {
                Debug.Log("No rankings found.");
                return;
            }
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log($"Document ID: {document.Id}");
                var rankingDict = document.ToDictionary();
                foreach (var pair in rankingDict)
                {
                    Debug.Log($"{pair.Key}: {pair.Value}");
                }
            }
        });
    }
}
