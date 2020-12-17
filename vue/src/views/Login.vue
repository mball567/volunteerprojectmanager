<template>
  <div id="login" class="text-center container">
    <div id="aside">
    <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Login</h1>
      <div class="alert alert-danger" role="alert" v-if="invalidCredentials">Invalid username and password!</div>
      <div class="alert alert-success" role="alert" v-if="this.$route.query.registration">Thank you for registering, please sign in.</div>
      <label id="forUsername" for="username" class="sr-only">Username: </label>
      <input type="text" id="username" class="form-control" placeholder="Username" v-model="user.username" required autofocus/> <br/>
      <label id="forPassword" for="password" class="sr-only">Password: </label>
      <input type="password" id="password" class="form-control" placeholder="Password" v-model="user.password" required /> <br/>
      <router-link id="needAccount" :to="{ name: 'register' }">Need an account?</router-link>
      <button id="signIn" type="submit">Sign in</button>
    </form>
    </div>

    <div id="imageOne">
      <img src="https://images.unsplash.com/photo-1560252811-2ab1e44ce202?ixlib=rb-1.2.1&ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&auto=format&fit=crop&w=334&q=80" alt="Volunteers akting together">
    </div>

    <div id="aktDescription">
      <p id="aktName">akt /akt/</p>
      <p id="aktVerb">verb</p>
      <p id="aktDef">To work together for a common cause and complete transformational projects by</p>
      <p id="aktMission"> acting kindly together</p>
    </div>
  </div>
</template>

<script>
import authService from "../services/AuthService";
import WebService from "../services/WebService.js"

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/profiles/my");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  },
  created(){
    WebService.getCauses()
      .then( (response) => {
      this.$store.commit("SET_CAUSES", response.data);
      });  
  },
};
</script>

<style scoped>
div#login{
  color: white;
}
.container{
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 1fr;
  grid-template-areas: 
  "aside imageOne akt ."
  ". . . .";
  height: 100vh;
}

div#aside{
  grid-area: aside;
}

.alert{
  color: white;
}

div#aside h1{
  margin-top: 0em;
  padding-left: 0.65em;
}

label#forUsername{
  
  margin-right: 0.25em;
  padding-left: 1.3em;
}

label#forPassword{
  
  margin-right: 0.25em;
  padding-left: 1.3em;
}

input#username{
  margin-block: 0.75em;
}

input#password{
  margin-top: 0.75em;
  margin-left: 0.5em;
  margin-bottom: 1.25em;
}

#needAccount{
  color:#ffb703ff;
  margin-right: 3.525em;
  margin-left: 1.25em
}

#needAccount:hover{
  color: white;
}

button#signIn{
  padding-block: 0.125em;
  padding-left: 0.5em;
  padding-right: 0.5em;
}

div#imageOne{
  grid-area: imageOne;
}

div#aktDescription{
  grid-area: akt;
}

p#aktName{
  font-size: 2em;
  font-weight: bold;
  margin-top: 0em;
  margin-left: 0.25em;
  margin-bottom: 0.25em;
}

p#aktVerb{
  font-size: 1.5em;
  font-style: italic;
  margin-left: 0.25em;
  margin-block: 0.25em;
}

p#aktDef{
  font-size: 1.25em;
  margin-left: 0.5em;
  margin-bottom: 0em;
  
}

p#aktMission{
  font-size: 1.25em;
  font-style: italic;
  font-weight: bold;
  margin-left: 0.5em;
  margin-top: 0.125em;
}

</style>

