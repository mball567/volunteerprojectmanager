<template>
  <div id="register" class="text-center">
    <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <label for="isOrganization">Are You an Organization?</label>
      <input
        type="checkbox"
        id="isOrganization"
        class="form-control"
        placeholder="isOrganization"
        v-model="checked"
        v-on:change="setIsOrganization"
        autofocus
      />
      <label for="username" class="sr-only">Username</label>
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      <label for="password" class="sr-only">Password</label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      <input
        type="password"
        id="confirmPassword"
        class="form-control"
        placeholder="Confirm Password"
        v-model="user.confirmPassword"
        required
      />
      <user-profile/>
      <router-link :to="{ name: 'login' }">Have an account?</router-link>
      <button class="btn btn-lg btn-primary btn-block" type="submit">Create Account</button>
    </form>
  </div>
</template>

<script>
import authService from '../services/AuthService';
import WebService from '../services/WebService.js';
import UserProfile from '../components/UserProfile.vue';

export default {
  name: 'register',
  components:{
    UserProfile,
  },
  data() {
    return {
      user: {
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
        isOrganization: false,
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
      checked: false,
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService.register(this.user)
        
          .then((response) => {
            if (response.status == 201) {
              WebService.createProfile(this.$store.state.user.profile);
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
    setIsOrganization() {
      if (this.checked === true){
        this.user.isOrganization = true;
      }
    },
  },
};
</script>

<style></style>
