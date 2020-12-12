<template>
  <div id="org-profile">
    <form class="form-register" @submit.prevent="register">
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <label id="username" for="username" class="sr-only">Username: </label>
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
      <label for="password" class="sr-only">Password: </label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      /><br />
      <label for="confirmPassword" class="sr-only">Confirm Password: </label>
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
      <input id="OrgName" type="text" placeholder="Organization" v-model="user.organization.OrgName" required />
      <br />

      <div class="orgBio">
      <label for="OrgBio">Organization Bio: </label>
      <textarea id="OrgBio" type="text" placeholder="Tell us why you're akting" rows="8" cols="40" v-model="user.organization.OrgBio" required /> <br />
      </div>

      

      <label for="OrgCity">City: </label>
      <input id="OrgCity" type="text" placeholder="City" v-model="user.organization.OrgCity" required /> <br />

      <label for="OrgState">State: </label>
      <input id="OrgState" type="text" placeholder="State" v-model="user.organization.OrgState" required />
      <br />

      <label for="orgZipCode">ZIP Code: </label>
      <input
        type="text"
        id="orgZipCode"
        placeholder="00000"
        pattern="[0-9]{5}"
        title="Five digit zip code"
        v-model="user.organization.OrgZip"
        required
      />
      <br />

      <label for="OrgContactEmail">E-Mail Address: </label>
      <input id="e-mail" type="email" placeholder="E-Mail Address" v-model="user.organization.OrgContactEmail" required /> <br />
      <router-link id="router-link" :to="{ name: 'login' }">Have an account?</router-link><br />
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
div#org-profile{
  display: block;
  text-align: left;
}

form{
  display: inline-block;
  margin-left: auto;
  margin-right: auto;
  text-align: left;
}

form label {
  color: white;
  padding-left: 1.4em;
  padding-right: 0.5em;
}

form input#username {
  margin-top: 2.25em;
}

form input#e-mail {
  margin-bottom: 1.25em;
}

form input {
  padding-top: 0.25em;
  padding-bottom: 0.25em;
  margin: 0.25em;
}

form textarea{
  padding-top: 0.25em;
  padding-bottom: 0.25em;
  margin: 0.25em;
}

.orgBio * {
  vertical-align: top;
}

div#org-profile form #router-link{
  color: white;
  padding-left: 1.4em;
  padding-top: 5em;
}

div#org-profile form button {
  margin-top: 1.75em;
  padding: 0.5em;
  margin-left: 1.5em;
}
</style>