<template>
  <div class="container">
    <body>
      <div>
        <main id="main-grid">
          <nav id="site-nav">
            <ul>
              <li><a href="#">My projects</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">My teams</a></li>
              &nbsp;|&nbsp;
              <li><a href="#">Akt with others</a></li>
              &nbsp;|&nbsp;
              <li><router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link></li>
              
              
            </ul>
          </nav>

          <aside>
            <h2 id="eventName">{{ event.eventName }}</h2>
            <p>Email: {{ event.eventContactEmail }}</p>
            <p>
              Currently akting in: {{ event.eventCity }},
              {{ event.eventState }}
            </p>
            <div>
            <p>Event Date: {{event.eventDate}}</p>
            <p>Event Start Time: {{event.eventStartTime}}</p>
            <p>Event End Time: {{event.eventEndTime}}</p>
            <p>Event Working Hours: {{event.eventWorkingHours}}</p>
            </div>
          <div id="button">
          <button v-on:click="eventSignUp">Sign Up</button>
          </div>
          </aside>

          <div id="bioArea">
            <h2>Description:</h2>
            <p id="bio">{{ event.eventDesc }}</p>
          </div>

          <div id="causes">
            <ul>
              <h2>People Attending This Event:</h2>
              <li v-for="profile in event.eventProfiles" v-bind:key="profile.userId" v-on:click="$router.push({path:`/profiles/${profile.userId}`})">{{profile.firstName}} {{profile.lastName}}</li>
            </ul>
          </div>

        </main>
      </div>
    </body>
  </div>
</template>

<script>
import WebService from '../services/WebService';
export default {
    name: "event",
    data () {
        return {
            event: {
                userId: 0,
                projId: 0,
                eventId: 0,
                eventName: "",
                eventDesc: "",
                eventZip: 0,
                eventCity: "",
                eventState: "", 
                eventStartTime: "",
                eventEndTime: "",
                eventDate: "",
                eventWorkingHours: 0,
                eventContactEmail: "",
                isOrg: false,
                eventProfiles : [],
            },
        }
    },
    methods : {
        getEvent(eventId){
            WebService.getEvent(eventId)
            .then( (response) => {
                this.event = response.data;
                this.event.userId = this.$store.state.user.userId;
            });
        },
        eventSignUp(){
          WebService.eventSignUp(this.event);
          this.$router.push({path:`/projects/${this.event.projId}`})
        }
    },
    created() {
        let eventId = this.$route.params.eventId;
        this.getEvent(eventId);
    },
}
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
    "aside bio causes";
}

/* body {
  background-color: #023047ff;
  font-family: "Montserrat", sans-serif;
  font-weight: 400;
} */

main div#bioArea {
  grid-area: bio;
  color: white;
  height: 300px;
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
  height: 300px;
  padding-left: 1em;
  padding-right: 2em;
}

aside h2#eventName{
  font-size: 45px;
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
  height: 300px;
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

div#causes ul li:hover {
color: #ffb703ff;
cursor: pointer;
font-weight: bold;
text-decoration: underline;
}
</style>