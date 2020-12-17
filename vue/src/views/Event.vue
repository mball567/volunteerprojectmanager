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
            <h2>{{ event.eventName }}</h2>
            <p>Email: {{ event.eventContactEmail }}</p>
            <p>
              Currently akting in: {{ event.eventCity }},
              {{ event.eventState }}
            </p>
          </aside>

          <div id="bioArea">
            <h2>Description:</h2>
            <p id="bio">{{ event.eventDesc }}</p>
            <div>
                <p>Event Date: {{event.eventDate}}</p>
                <p>Event Start Time: {{event.eventStartTime}}</p>
                <p>Event End Time: {{event.eventEndTime}}</p>
                <p>Event Working Hours: {{event.eventWorkingHours}}</p>
            </div>
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
            },
        }
    },
    methods : {
        getEvent(eventId){
            WebService.getEvent(eventId)
            .then( (response) => {
                this.event = response.data;
            });
        },
    },
    created() {
        let eventId = this.$route.params.projId;
        this.getEvent(eventId);
    },
}
</script>

<style>

</style>