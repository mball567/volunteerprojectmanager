<template>
  <div class="container">
    <body>
      <div id="project-view">
        <main id="main-grid">
          <nav id="site-nav">
            <ul>
              <li><a href="#">My Profile</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">Akt with others</a></li>
              &nbsp;|&nbsp;
              <li><router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link></li>
            </ul>
          </nav>

          <aside>
            <img id="userImage" :src="project.projImage" />
            <h2>{{ project.projName }}</h2>
            <p>Email: {{ project.projContactEmail }}</p>
            <p>
              Currently akting in: {{ project.projCity }},
              {{ project.projState }}
            </p>
          </aside>

          <p id="bio">{{ project.projDesc }}</p>

          <div id="causes">
            <ul>
              <li v-for="causeName in project.projCauseNames" v-bind:key="causeName">{{causeName}}</li>
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
      project: {
                userId: 0,
                profId: 0,
                projName: "",
                projDesc: "",
                projImage: "",
                projZip: 0,
                projCity: "",
                projState: "",
                projWorkingHours: 0,
                projContactEmail: "",
                projCauseNames: [],
            },
    };
  },
  methods: {
    getProjectInfo(projId) {
      WebService.getProjectInfo(projId).then((response) => {
        this.project = response.data;
      });
    },
  },
  created() {
    let projId = this.$route.params.projId;
    this.getProjectInfo(projId);    
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