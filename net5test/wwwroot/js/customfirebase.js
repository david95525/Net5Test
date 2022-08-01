
// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
// personal account

// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
        apiKey: "AIzaSyCsN8sNgu7IhEZxSv31RZddJCdRJYh1zWY",
        authDomain: "peppy-strategy-335213.firebaseapp.com",
        projectId: "peppy-strategy-335213",
        storageBucket: "peppy-strategy-335213.appspot.com",
        messagingSenderId: "506901322194",
        appId: "1:506901322194:web:9c3bf285da818c0860d00b",
        measurementId: "G-21ZRTJMEB4"
   };
const firebaseConfig2 = {
    apiKey: "AIzaSyAlZTJnRDe6CbN8HAh6Xr6yQS4XNyB-qfU",
    authDomain: "microlife-connected-health.firebaseapp.com",
    databaseURL: "https://microlife-connected-health.firebaseio.com",
    projectId: "microlife-connected-health",
    storageBucket: "microlife-connected-health.appspot.com",
    messagingSenderId: "58101494323",
    appId: "1:58101494323:web:f8cd2d5a3ef6b235aca946"
}
    firebase.initializeApp(firebaseConfig2);
firebase.analytics();

$(function () {
    $("body").loading();
    setTimeout(() => { $("body").loading("stop") }, 10000);
    firebase.auth()
        .getRedirectResult()
        .then((result) => {
            if (result.credential) {
                var thridpartyType = 0;
                /** type {firebase.auth.OAuthCredential} */
                var credential = result.credential;
                // This gives you a Google Access Token. You can use it to access the Google API.
                var providerid = credential.providerId;
               
                var idToken = null;
                switch (providerid) {
                    case "facebook.com":
                        thridpartyType = 2;
                        break;
                    case "google.com":
                        thridpartyType = 1;
                        break;
                    case "apple.com":
                        thridpartyType = 3;
                        idToken = credential.idToken;
                        break;
                }
                var model =
                {
                    Name: result.additionalUserInfo.profile.name,
                    Email: result.user.email,
                    Id: result.additionalUserInfo.profile.id,
                    AccessToken: result.credential.accessToken,
                    IdToken: idToken,
                    ThridpartyType: thridpartyType
                }
                loginsuccessredirect(model);
            }
            else {
                $("body").loading("stop");
            }
        }).catch((error) => {
            // Handle Errors here.
            var errorCode = error.code;
            var errorMessage = error.message;
            // The email of the user's account used.
            var email = error.email;
            // The firebase.auth.AuthCredential type that was used.
            var credential = error.credential;
            // ...
            console.log(errorCode, errorMessage, email, credential);
            $("body").loading("stop");
            alert(errorMessage);
        });

});
function googleLogin() {
    var provider = new firebase.auth.GoogleAuthProvider();
    firebase.auth().signInWithRedirect(provider);

}

function facebookLogin() {
    var provider = new firebase.auth.FacebookAuthProvider();
    firebase.auth().signInWithRedirect(provider);
}

function appleLogin() {
    var provider = new firebase.auth.OAuthProvider('apple.com');
    firebase.auth().signInWithRedirect(provider);
}


function loginsuccessredirect(model) {
    $.ajax({
        type: "POST",
        url: '/account/thirdlogin',
        data: model,
        dataType: 'json',
        success: function (rsp) {
            if (rsp.result == false)
                alert(rsp.message);
            else {
                location.href = rsp.redirecturl;
            }
        },
        error: function () {
        },
        complete: function () {
            $("body").loading("stop");
        }
    });
}