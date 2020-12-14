import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token');
const currentUser = JSON.parse(localStorage.getItem('user'));
const currentCauses = JSON.parse(localStorage.getItem('causes'));

if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    causes: currentCauses || [],
    userImage: '',
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      state.causes = [];
      state.userImage = '';
      axios.defaults.headers.common = {};
    },
    ADD_PROFILE(state, profile){
      // set the user ID to the user.userId
      state.user.profile = profile;
    },
    ADD_ORG(state, organization){
      state.user.organization = organization;
    },
    SET_CAUSES(state, causes){
      state.causes = causes;
      localStorage.setItem('causes', JSON.stringify(causes));
    },
    SET_USERIMAGE(state, imageString){
      state.userImage = imageString;
    },
  }
})
