// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import {getAuth} from "firebase/auth";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyDahRnx0Eh9APvhzNDoUQdXdS_9JIScU7s",
  authDomain: "lookup-42ced.firebaseapp.com",
  projectId: "lookup-42ced",
  storageBucket: "lookup-42ced.firebasestorage.app",
  messagingSenderId: "275015081052",
  appId: "1:275015081052:web:53c121af8b7c217ef55a99"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const auth = getAuth(app);

export { auth };