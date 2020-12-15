<template>
  <div class="container">
    <body>
      <div id="team-view">
        <main id="main-grid">
          <nav id="site-nav">
            <ul>
              <li><a href="#">My Profile</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">Team Projects</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">Akt with others</a></li>
              &nbsp;|&nbsp;
              <li><router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link></li>
            </ul>
          </nav>

          <aside>
            <img id="userImage" :src="team.teamImage" />
            <h2>{{ team.teamName }}</h2>
            <p>Email: {{ team.teamContactEmail }}</p>
            <p>
              Currently akting in: {{ team.teamCity }},
              {{ team.teamState }}
            </p>
          </aside>

          <p id="bio">{{ team.teamBio }}</p>

          <div id="causes">
            <ul>
              <li v-for="causeName in team.teamCauseNames" v-bind:key="causeName">{{causeName}}</li>
            </ul>
          </div>

        </main>
      </div>
    </body>
  </div>
</template>

<script>
import WebService from "../services/WebService.js";
export default {
  name: "profile",
  data() {
    return {
      team: {
                teamName: "",
                teamImage: "",
                teamBio: "",
                teamZip: "",
                teamCity: "",
                teamState: "",
                teamContactEmail: "",
                teamMembers: [],
                teamCauseNames: [],
            },
    };
  },
  methods: {
    getTeamInfo(teamId) {
      WebService.getTeamInfo(teamId).then((response) => {
        this.team = response.data;
      });
    },
  },
  created() {
    let teamId = this.$route.params.teamId;
    this.getTeamInfo(teamId);    
  },
};
</script>

<style>
/* color
--light-cornflower-blue: #8ecae6ff;
--blue-green: #219ebcff;
--prussian-blue: #023047ff;
--honey-yellow: #ffb703ff;
--orange: #fb8500ff;
*/

@import url("https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap");

#main-grid {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas:
    "nav nav nav"
    "aside bio bio"
    "aside causes causes";
}

body {
  background-color: #023047ff;
  font-family: "Montserrat", sans-serif;
  font-weight: 400;
}

main p#bio {
  grid-area: bio;
  color: white;
}

main nav#site-nav {
  grid-area: nav;
  width: 100%;
  text-align: right;
}

main nav#site-nav ul {
  display: flex;
  align-items: stretch;
  justify-content: space-between;
}

aside {
  color: white;
  grid-area: aside;
  height: 100vh;
}

nav#site-nav li {
  list-style-type: none;
  display: inline;
}

nav#site-nav li a {
  list-style-type: none;
  color: #ffb703ff;
  padding: 20px;
  text-decoration: none;
  font-weight: bold;
}

nav#site-nav li a:hover {
  color: #219ebcff;
}

img#userImage {
  border-radius: 50%;
  width: 200px;
  height: 200px;
}
</style>