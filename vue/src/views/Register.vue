<template>
  <div id="register" class="text-center">
    <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
    <label id="isOrg" for="isOrganization">Are you registering as an Organization?</label>
    <input
      type="checkbox"
      id="isOrganization"
      class="form-control"
      placeholder="isOrganization"
      v-model="checked"
      autofocus
    />
    <br />
    <user-profile v-show="checked === false"/>
    <organization-profile v-show="checked === true"/>
  </div>
</template>

<script>
import OrganizationProfile from '../components/OrganizationProfile.vue';
import UserProfile from "../components/UserProfile.vue";
import authService from "../services/AuthService";

export default {
  name: "register",
  components: {
    UserProfile,
    OrganizationProfile,
  },
  data(){
    return {
      checked: false
   }
  },
  methods: {
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
/* color
--light-cornflower-blue: #8ecae6ff;
--blue-green: #219ebcff;
--prussian-blue: #023047ff;
--honey-yellow: #ffb703ff;
--orange: #fb8500ff;
*/

@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap');

div#register h1{
  color: white;
  margin-block: 0.5em;
  padding-left: 0.75em;
}

div#register label#isOrg {
  color: white;
  padding-left: 1.5em;
}






</style>
