<template>
  <div>
    <form class="form-register" @submit.prevent="register, addOrg">
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
      <label for="orgName">Organization Name: </label>
      <input id="orgName" type="text" v-model="organization.orgName" required />
      <br />

      <label for="orgBio">Organization Bio: </label>
      <input id="orgBio" type="text" v-model="organization.orgBio" required /> <br />

      <label for="orgZipCode">ZIP Code (5 digits):</label>
      <input
        type="text"
        id="orgZipCode"
        pattern="[0-9]{5}"
        title="Five digit zip code"
        v-model="organization.orgZip"
        required
      />
      <br />

      <label for="orgCity">City: </label>
      <input id="orgCity" type="text" v-model="organization.orgCity" required /> <br />

      <label for="orgState">State: </label>
      <input id="orgState" type="text" v-model="organization.orgState" required />
      <br />

      <label for="orgContactEmail">E-Mail Address: </label>
      <input type="email" v-model="organization.orgContactEmail" required /> <br />
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
      organization: {
        orgName: "",
        orgImage: "",
        orgBio: "",
        orgZip: 0,
        orgCity: "",
        orgState: "",
        orgContactEmail: "",
      },
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
        isOrganization: true,
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    addOrg() {
        this.$store.commit("ADD_ORG", this.organization);
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
  }
}
</script>

<style scoped>
</style>