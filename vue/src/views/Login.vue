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

    <div id="imageTwo"><img src="https://images.unsplash.com/photo-1603855795444-174913c12916?ixlib=rb-1.2.1&ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&auto=format&fit=crop&w=2000&q=80" alt="Volunteers also akting together"></div>

    <div id="imageThree">
      <img src="https://images.unsplash.com/photo-1607227063002-677dc5fdf96f?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1934&q=80" alt="Volunteers also akting together also">
    </div>

    <div id="imageFour">
      <img src="https://images.unsplash.com/photo-1589138348980-f4efa5ff18dc?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1953&q=80" alt="Also volunteers also akting together too">
    </div>

    <div id="aktDescription">
      <p id="aktName">akt /akt/</p>
      <p id="aktVerb">verb</p>
      <p id="aktDef">To work together for a common cause and complete transformational projects by <b>acting kindly together</b></p>
    </div>

    <div id="aktMissionStatementOne">
      <p> We're on a mission to mobilize the world's talent for good.</p>
      <p>Akt is an online platform that matches professionals who want to donate their time with nonprofits who need their skills.</p>
    </div>

    <div id="aktMissionStatementTwo">
      <p>Search for ongoing <b>projects</b> or one-off <b>events</b>. Or join up with other like-minded individuals to form a <b>team</b>.</p>
      <p>From walking rescue dogs to teaching girls to code, our mission-driven <b>organizations</b> have collectively worked with hundreds of Akt volunteers.</p>
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
  "aside imageOne akt imageTwo"
  "imageThree missionOne imageFour missionTwo";
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

div#imageOne img{
  grid-area: imageOne;
  border-radius: 8px;
  width: 100%;
  height: 400px;
}

div#imageTwo img{
  grid-area: imageTwo;
  border-radius: 8px;
  width: 100%;
  height: 400px;
}

div#imageThree img{
  grid-area: imageThree;
  border-radius: 8px;
  width: 100%;
  height: 400px;
  
}

div#imageFour img{
  grid-area: imageFour;
  border-radius: 8px;
  width: 100%;
  height: 400px;
}

div#aktDescription{
  grid-area: akt;
  display: flex;
  flex-direction: column;
  align-items: space-between;
  margin-left: 0.25em;
  margin-right: 0.25em;
}

p#aktName{
  font-size: 2em;
  font-weight: bold;
  margin-top: 0em;
  margin-left: 0.5em;
  margin-bottom: 0.25em;
}

p#aktVerb{
  font-size: 1.5em;
  font-style: italic;
  margin-left: 0.6em;
  margin-block: 0.25em;
}

p#aktDef{
  font-size: 1.25em;
  margin-left: 0.8em;
  margin-right: 0.5em;
  margin-bottom: 0em;
}

div#aktMissionStatementOne{
  grid-area: missionOne;
  font-size: 1.25em;
  margin-left: 1em;
  margin-right: 1em;
}

div#aktMissionStatementTwo{
  grid-area: missionTwo;
  font-size: 1.25em;
  margin-left: 1em;
  margin-right: 1em;
}

</style>

