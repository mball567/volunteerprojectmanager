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
      <label for="OrgName">Organization Name: </label>
      <input id="OrgName" type="text" v-model="user.organization.OrgName" required />
      <br />

      <label for="OrgBio">Organization Bio: </label>
      <input id="OrgBio" type="text" v-model="user.organization.OrgBio" required /> <br />

      <label for="orgZipCode">ZIP Code (5 digits):</label>
      <input
        type="text"
        id="orgZipCode"
        pattern="[0-9]{5}"
        title="Five digit zip code"
        v-model="user.organization.OrgZip"
        required
      />
      <br />

      <label for="OrgCity">City: </label>
      <input id="OrgCity" type="text" v-model="user.organization.OrgCity" required /> <br />

      <label for="OrgState">State: </label>
      <input id="OrgState" type="text" v-model="user.organization.OrgState" required />
      <br />

      <label for="OrgContactEmail">E-Mail Address: </label>
      <input type="email" v-model="user.organization.OrgContactEmail" required /> <br />
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
      // organization: {
      //   OrgName: "",
      //   OrgImage: "",
      //   OrgBio: "",
      //   OrgZip: 0,
      //   OrgCity: "",
      //   OrgState: "",
      //   OrgContactEmail: "",
      // },
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
        isOrganization: true,
        organization: {
          OrgName: "",
          OrgImage: "",
          OrgBio: "",
          OrgZip: 0,
          OrgCity: "",
          OrgState: "",
          OrgContactEmail: "",
       },
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