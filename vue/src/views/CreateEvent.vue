<template>
  <div id="create-event">
    <h1>Create Event</h1>
      <form class="form-createevent" @submit.prevent="createEvent">
      <label id="eventName" for="eventName" class="sr-only">Event Name: </label>
      <input
        type="text"
        id="eventName"
        class="form-control"
        placeholder="Event Name"
        v-model="event.eventName"
        required
        autofocus
      /> <br/>

      <label>Drag and drop your Event image here:</label>
      <drop-zone id="dropzone"/>

      <div class="eventDescription">
      <label for="eventDesc" class="sr-only">Event Description: </label> <br/>
      <textarea
        type="text"
        id="eventDesc"
        class="form-control"
        rows="8" 
        cols="40"
        placeholder="How will this event akt?"
        v-model="event.eventDesc"
        required
      />
      </div>

      <label for="eventDate">Event Date: </label>
      <input id="eventDate" type="date" v-model="event.eventDate" required />
      <br />
      
      <label for="eventStartTime">Event Start Time: </label>
      <input id="eventStartTime" type="time" v-model="event.eventStartTime" required />
      <br />

            
      <label for="eventEndTime">Event End Time: </label>
      <input id="eventEndTime" type="time" v-model="event.eventEndTime" required />
      <br />

      <label for="eventHours">Estimated hours for event completion: </label>
      <input id="eventHours" type="number" placeholder="Hours To Complete" v-model="event.eventWorkingHours" required />
      <br />
      
      <label for="eventCity">City: </label>
      <input id="eventCity" type="text" placeholder="City" v-model="event.EventCity" required />
      <br />

      <label for="eventState">State: </label>
      <input id="evnetState" type="text" placeholder="State" v-model="event.EventState" required />
      <br />

      <label for="eventZip" class="sr-only">Zip Code: </label>
      <input
        type="text"
        id="eventZip"
        class="form-control"
        placeholder="Zip Code"
        v-model="event.EventZip"
        required
      />
      <br />

      <label for="e-mail">Lead Contact E-Mail: </label>
      <input id="e-mail" type="email" placeholder="E-Mail Address" v-model="event.EventContactEmail" required /> <br />

      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Event
      </button>
    </form>
  </div>
</template>

<script>
import WebService from '../services/WebService.js'
import DropZone from '../components/DropZone.vue'
export default {
    components: {
      DropZone,
    },  
    data() {
        return{
            event: {
                userId: 0,
                eventId: 0,
                projId: 0,
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
            },
        };
    },
    methods: {
        createEvent(){
          // this.event.UserId = this.$store.state.user.userId;
          // this.event.projId = this.$route.params.projId;

          WebService  
          .createEvent(this.event)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/profiles/my",
              });
            }
          })
        }
    },
    created(){
          this.event.UserId = this.$store.state.user.userId;
          let projectId = parseInt(this.$route.params.projId);
          this.event.projId = projectId;
    },
}
</script>

<style scoped>

div#create-event h1{
  color: white;
  margin-block: 0.5em;
  padding-left: 0.75em;
}

div#create-event{
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

form input#eventName {
  margin-top: 2.25em;
}

#dropzone{
  margin-left: 1.5em;
  margin-block: 0.5em;
}

textarea#eventDesc{
  margin-bottom: 0.5em;
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
  margin-top: 0.5em;
  margin-left: 1.75em;
}

.eventDescription * {
  vertical-align: top;
}

div#user-profile form #router-link{
  color: white;
  padding-left: 1.4em;
  padding-top: 5em;
}

div#create-event form button {
  margin-top: 1.5em;
  padding: 0.5em;
  margin-left: 1.5em;
}

p#causeSelect{
  padding-left: 0.2em;
}

form div#causeList {
  color: white;
  padding-left: 1.25em;
}
</style>