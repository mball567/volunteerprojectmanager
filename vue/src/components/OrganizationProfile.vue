<template>
  <div id="create-org" class="container">
    <div id="createForm">
      <form class="form-register" @submit.prevent="register">
        <div class="alert alert-danger" role="alert" v-if="registrationErrors">{{ registrationErrorMsg }}</div>
          <!-- <label id="username" for="username" class="sr-only">Username: </label> -->
          <input type="text" id="username" class="form-control" placeholder="Username" v-model="user.username" required autofocus/>
          <!-- <label for="password" class="sr-only">Password: </label> -->
          <input type="password" id="password" class="form-control" placeholder="Password" v-model="user.password" required />
          <!-- <label for="confirmPassword" class="sr-only">Confirm Password: </label> -->
          <input type="password" id="confirmPassword" class="form-control" placeholder="Confirm Password" v-model="user.confirmPassword" required/>
          <!-- <label for="OrgName">Organization Name: </label> -->
          <input id="OrgName" type="text" placeholder="Organization Name" v-model="user.organization.OrgName" required />
          <!-- <label>Drag or drop your Organization logo here</label> -->
      
          <drop-zone id="dropzone"/>
      
          <!-- <div class="orgBio"> -->
          <!-- <label for="OrgBio">Organization Bio: </label> -->
          <textarea id="OrgBio" type="text" placeholder="Tell us why your organization is akting" rows="8" cols="40" v-model="user.organization.OrgBio" required />
          <!-- </div> -->
          <!-- <label for="OrgCity">City: </label> -->
          <input id="OrgCity" type="text" placeholder="City" v-model="user.organization.OrgCity" required />
          <!-- <label for="OrgState">State: </label> -->
          <input id="OrgState" type="text" placeholder="State" v-model="user.organization.OrgState" required />
          <!-- <label for="orgZipCode">ZIP Code: </label> -->
          <input type="text" id="orgZipCode" placeholder="Zip Code" pattern="[0-9]{5}" title="Five digit zip code" v-model="user.organization.OrgZip" required />
          <!-- <label for="OrgContactEmail">E-Mail Address: </label> -->
          <input id="e-mail" type="email" placeholder="E-Mail Address" v-model="user.organization.OrgContactEmail" required /> 
      

          <p id="causeSelect">Select all causes that apply to this Organization:</p>
          <div id="causeList">         
          <div class="cause" v-for="cause in causes" v-bind:key="cause.causeId">        
            <input type="checkbox" v-bind:id="cause.causeId" v-bind:value="cause.causeId" v-model="user.organization.OrgCauses"/>
              {{cause.causeName}}
          </div>
          </div>
      
         

          <div id="buttonDiv">
             <router-link id="haveAccount" :to="{ name: 'login' }">Have an account?</router-link><br/>
          <button class="btn btn-lg btn-primary btn-block" type="submit">Create Account</button>
          </div>
        </form>
      </div>
    </div>
</template>

<script>
import authService from "../services/AuthService";
import DropZone from "./DropZone.vue";

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
        isOrganization: true,
        organization: {
          OrgName: "",
          OrgImage: "",
          OrgBio: "",
          OrgZip: 0,
          OrgCity: "",
          OrgState: "",
          OrgContactEmail: "",
          OrgCauses: [],
       },
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
      causes: [],
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
        this.user.organization.OrgImage = this.$store.state.userImage;

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
  created (){
        this.causes = this.$store.state.causes;
  },
}
</script>

<style scoped>
div#create-org.container{
  display: grid;
  grid-template-columns: 375px 1fr 375px;
  grid-template-areas: 
  ". form .";
}

@media screen and (max-width: 1024px){
div#create-org.container{
  grid-template-columns: 200px 1fr 200px;
}
}

@media screen and (max-width: 768px){
div#create-org.container{
  grid-template-columns: 100px 1fr 100px;
}
}

div#createForm{
  grid-area: form;
}

div#create-project h1{
  color: white;
  margin-block: 0.5em;
  padding-left: 0.75em;
  text-align: center;
}

div#create-project{
  display: block;
  text-align: left;
}

input{
  width: 100%;
  height: 40px;
  border-radius: 4px; 
  background-color: #fff; 
  box-shadow: 1px 2px 5px rgba(0,0,0,.5); 
  border: solid 1px #cbc9c9;
  margin-top: 0.75em; 
}

input[type="checkbox"]{
  width: 20px;
  height: 20px;
}

div#causeList{
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
  padding-bottom: 1.5em;
}

div.cause{
  width: 225px;
}

input,
textarea,
select{
  font-family: 'Montserrat', sans-serif;
  font-size: 16px;
  color: #4c4c4c;
}

#dropzone{
  font-family: 'Montserrat', sans-serif;
  font-size: 16px;
  color: #4c4c4c;
  width: 100%;
  margin-top: 0.75em;
  border-radius: 4px;
}

textarea#OrgBio{
  border-radius: 4px;
  width: 100%;
  margin-top: 0.75em;
  margin-bottom: 0em;
}

div#create-org form button {
  margin-top: 1.5em;
  padding: 0.5em;
  margin-left: auto;
  margin-right: auto;
  text-align: center;
}

#haveAccount{
  color:#ffb703ff;
  text-align: center;
}

#haveAccount:hover{
  color: white;
}

#buttonDiv{
  width: 100%;
  text-align: center;
}

p#causeSelect{
  font-size: 1.5em;
  padding-left: 0.2em;
  font-weight: bold;
  width: 100%;
}

form div#causeList {
  color: white;
  padding-left: 1.25em;
  
}

</style>