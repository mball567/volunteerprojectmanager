<template>
  <div class="container">
    <body>
      <div>
        <main id="main-grid">
          <nav id="site-nav">
            <ul>
              <li><router-link v-bind:to="{ name: 'createproject' }">Create A Project</router-link></li>
              &nbsp;|&nbsp;
              <li><router-link v-bind:to="{ name: 'search' }">Search</router-link></li>
              &nbsp;|&nbsp;
              <li id="logout"><router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link></li>
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
            <div id="button">
            <button v-on:click="createEventForProject">Create An Event</button>
            </div>
          </aside>

          <div id="bioArea">
          <h2>Team Description:</h2>
          <p id="bio">{{ project.projDesc }}</p>
          </div>




          
          <div id="causes">
            <ul>
              <h2>Causes:</h2>
              <li v-for="causeName in project.projCauseNames" v-bind:key="causeName">{{causeName}}</li>
            </ul>
          </div>

          <div id="events">
            <ul>
              <h2>Events:</h2>
              <li v-for="event in project.projEvents" v-bind:key="event.eventId" v-on:click="$router.push({path:`/projects/${project.projId}/events/${event.eventId}`})">{{event.eventName}}</li>
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
                projId: 0,
                projName: "",
                projDesc: "",
                projImage: "",
                projZip: 0,
                projCity: "",
                projState: "",
                projWorkingHours: 0,
                projContactEmail: "",
                projCauseNames: [],
                projEvents: [],
            },
    };
  },
  methods: {
    getProjectInfo(projId) {
      WebService.getProjectInfo(projId).then((response) => {
        this.project = response.data;
      });
    },
    createEventForProject(){
    if(this.project.userId === this.$store.state.user.userId){
      this.$router.push({path:`/projects/${this.project.projId}/events`});
    }
  },
  },
  created() {
    let projId = this.$route.params.projId;
    this.getProjectInfo(projId);    
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

@import url("https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap");

#main-grid {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas:
    "nav nav nav"
    "aside bio causes"
    "aside events .";
}

/* body {
  background-color: #023047ff;
  font-family: "Montserrat", sans-serif;
  font-weight: 400;
} */

main div#bioArea {
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
  justify-content: space-evenly;
}

aside {
  color: white;
  grid-area: aside;
  height: 100vh;
  padding-left: 1em;
  padding-right: 2em;
}


nav#site-nav{
  list-style-type: none;
  display: inline;
  background: white;
  margin-bottom: 65px;
}

nav#site-nav li {
  list-style-type: none;
  display: inline;
}

nav#site-nav li a {
  list-style-type: none;
  color: #023047ff;
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

div#causes {
  grid-area: causes;
  color: white;
}

div#causes ul {
  color: white;
  list-style-type: none;
  padding: 0;
}

div#events {
  grid-area: events;
  color: white;
}

div#events ul {
  color: white;
  list-style-type: none;
  padding: 0;
}
div#button button{
  font-size: 16px;
  width: 50%;
}

div#events ul li:hover {
color: #ffb703ff;
cursor: pointer;
font-weight: bold;
text-decoration: underline;
}
</style>