<template>
  <div>
    <form class="form-register" @submit.prevent="register">
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
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
      <br />
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
      <br />
      <label for="FirstName">First Name: </label>
      <input id="FirstName" type="text" v-model="profile.FirstName" required />
      <br />

      <label for="LastName">Last Name: </label>
      <input id="LastName" type="text" v-model="profile.LastName" required />
      <br />

      <label for="Bio"> Bio: </label>
      <input id="Bio" type="text" v-model="profile.Bio" required /> <br />

      <label for="zipCode">ZIP Code (5 digits):</label>
      <input
        type="text"
        id="zipCode"
        pattern="[0-9]{5}"
        title="Five digit zip code"
        v-model="profile.ProfZip"
        required
      />
      <br />
      <label for="city">City: </label>
      <input id="city" type="text" v-model="profile.ProfCity" required /> <br />

      <label for="state">State: </label>
      <input id="state" type="text" v-model="profile.ProfState" required />
      <br />
      <label for="e-mail">E-Mail Address: </label>
      <input type="email" v-model="profile.ProfContactEmail" required /> <br />
      <router-link :to="{ name: 'login' }">Have an account?</router-link>
      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "user-profile",
  props: {},
  data() {
    return {
      profile: {
        FirstName: "",
        LastName: "",
        Bio: "",
        ProfileImage: "",
        ProfZip: 0,
        ProfCity: "",
        ProfState: "",
        ProfContactEmail: "",
      },
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
        isOrganization: false,
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    addUserProfile() {
      if (this.checked === true) {
        this.$store.commit("ADD_PROFILE", this.profile);
      } else {
        this.profile = {
          FirstName: "",
          LastName: "",
          Bio: "",
          ProfileImage: "",
          ProfZip: 0,
          ProfCity: "",
          ProfState: "",
          ProfContactEmail: "",
        };
      }
    },
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/login",
                query: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style scoped>
</style>