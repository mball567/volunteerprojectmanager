<template>
  <div id="user-profile">
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
      <label for="FirstName">First Name: </label>
      <input id="FirstName" type="text" placeholder="First Name" v-model="user.profile.FirstName" required />
      <br />

      <label for="LastName">Last Name: </label>
      <input id="LastName" type="text" placeholder="Last Name" v-model="user.profile.LastName" required />
      <br />

      <label>Drag or drop your Profile image here:</label>
      <drop-zone id="dropzone"/>

      <div class="userBio">
      <label id="bioLabel" for="Bio">User Bio: </label>
      <textarea id="Bio" type="text" placeholder="Tell us why you're akting" rows="8" cols="40" v-model="user.profile.Bio" required /> <br />
      </div>

      <label for="city">City: </label>
      <input id="city" type="text" placeholder="City" v-model="user.profile.ProfCity" required /> <br />

      <label for="state">State: </label>
      <input id="state" type="text" placeholder="State" v-model="user.profile.ProfState" required />
      <br />

      <label for="zipCode">ZIP Code:</label>
      <input
        type="text"
        id="zipCode"
        placeholder="00000"
        pattern="[0-9]{5}"
        title="Five digit zip code"
        v-model="user.profile.ProfZip"
        required
      />
      <br />

      <label for="e-mail">E-Mail Address: </label>
      <input type="email" id="e-mail" placeholder="E-Mail Address" v-model="user.profile.ProfContactEmail" required /> <br />

      <div id="causeList"> 
      <p>Select all causes in which you are interested:</p>
      <tr v-for="cause in causes" v-bind:key="cause.causeId">
        <td>
          <input type="checkbox" v-bind:id="cause.causeId" v-bind:value="cause.causeId" v-model="user.profile.ProfCauses">
        </td>
        <td>
          {{cause.causeName}}
        </td>
      </tr>
      </div>

      <router-link id="router-link" :to="{ name: 'login' }">Have an account?</router-link><br />
      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";
import DropZone from './DropZone.vue';

export default {
  components: { DropZone },
  name: "user-profile",
  props: {},
  data() {
    return {
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
        isOrganization: false,
        profile: {
          FirstName: "",
          LastName: "",
          Bio: "",
          ProfileImage: "",
          ProfZip: 0,
          ProfCity: "",
          ProfState: "",
          ProfContactEmail: "",
          ProfCauses: [],
       },
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
      causes: [],
    };
  },
  methods: {
    // addUserProfile() {
    //   if (this.checked === true) {
    //     this.$store.commit("ADD_PROFILE", this.profile);
    //   } else {
    //     this.profile = {
    //       FirstName: "",
    //       LastName: "",
    //       Bio: "",
    //       ProfileImage: "",
    //       ProfZip: 0,
    //       ProfCity: "",
    //       ProfState: "",
    //       ProfContactEmail: "",
    //     };
    //   }
    // },
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        this.user.profile.ProfileImage = this.$store.state.userImage;

        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$store.state.userImage = "";
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
  created(){
    this.causes = this.$store.state.causes;
  },
};
</script>

<style scoped>

div#user-profile{
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

#dropzone{
  margin-left: 1.5em;
  margin-block: 0.5em;
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

.userBio * {
  vertical-align: top;
}

div#user-profile form #router-link{
  color: white;
  padding-left: 1.4em;
  padding-top: 0em;
}

div#user-profile form button {
  margin-top: 1.75em;
  padding: 0.5em;
  margin-left: 1.5em;
}

form div#causeList {
  color: white;
}

</style>