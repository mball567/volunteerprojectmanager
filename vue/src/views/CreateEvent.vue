<template>
  <div id="create-event" class="container"> 
    <div id="createForm">
      <h1>Create Event</h1>
      <form class="form-createevent" @submit.prevent="createEvent">
      <!-- <label id="eventName" for="eventName" class="sr-only">Event Name: </label> -->
      <input
        type="text"
        id="eventName"
        class="form-control"
        placeholder="Event Name"
        v-model="event.eventName"
        required
        autofocus
      /> <br/>

      <!-- <label>Drag and drop your Event image here:</label> -->
      <drop-zone id="dropzone"/>

      <div class="eventDescription">
      <!-- <label for="eventDesc" class="sr-only">Event Description: </label> <br/> -->
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

      <!-- <label for="eventDate">Event Date: </label> -->
      <input id="eventDate" type="date" v-model="event.eventDate" required />
      <br />
      
      <!-- <label for="eventStartTime">Event Start Time: </label> -->
      <input id="eventStartTime" type="time" v-model="event.eventStartTime" required />
      <br />

            
      <!-- <label for="eventEndTime">Event End Time: </label> -->
      <input id="eventEndTime" type="time" v-model="event.eventEndTime" required />
      <br />

      <!-- <label for="eventHours">Estimated hours for event completion: </label> -->
      <input id="eventHours" type="number" placeholder="Hours To Complete" v-model="event.eventWorkingHours" required />
      <br />
      
      <!-- <label for="eventCity">City: </label> -->
      <input id="eventCity" type="text" placeholder="City" v-model="event.EventCity" required />
      <br />

      <!-- <label for="eventState">State: </label> -->
      <input id="evnetState" type="text" placeholder="State" v-model="event.EventState" required />
      <br />

      <!-- <label for="eventZip" class="sr-only">Zip Code: </label> -->
      <input
        type="text"
        id="eventZip"
        class="form-control"
        placeholder="Zip Code"
        v-model="event.EventZip"
        required
      />
      <br />

      <!-- <label for="e-mail">Lead Contact E-Mail: </label> -->
      <input id="e-mail" type="email" placeholder="E-Mail Address" v-model="event.EventContactEmail" required /> <br />

      <div id="buttonDiv">
      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Event
      </button>
      </div>
    </form>
  </div>
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
                isOrg: false,
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
                path: `/projects/${this.event.projId}`,
              });
            }
          })
        }
    },
    created(){
          this.event.UserId = this.$store.state.user.userId;
          let projectId = parseInt(this.$route.params.projId);
          this.event.projId = projectId;

          if(this.$store.state.user.organization !== null){
            this.event.isOrg = true;
          }
          else{
            this.event.isOrg = false;
          }
    },
}
</script>

<style scoped>

div#create-event.container{
  display: grid;
  grid-template-columns: 375px 1fr 375px;
  grid-template-areas: 
  ". form .";
}

@media screen and (max-width: 1024px){
div#create-event.container{
  grid-template-columns: 200px 1fr 200px;
}
}

@media screen and (max-width: 768px){
div#create-event.container{
  grid-template-columns: 100px 1fr 100px;
}
}

div#createForm{
  grid-area: form;
}

div#create-event h1{
  color: white;
  margin-block: 0.5em;
  padding-left: 0.75em;
  text-align: center;
}

div#create-event{
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


input,
textarea,
select{
  font-family: 'Montserrat', sans-serif;
  font-size: 16px;
  color: #4c4c4c;
}

form label {
  color: white;
  padding-left: 1.4em;
  padding-right: 0.5em;
}

#dropzone{
  font-family: 'Montserrat', sans-serif;
  font-size: 16px;
  color: #4c4c4c;
  width: 100%;
  margin-top: 0.75em;
  border-radius: 4px;
}

textarea#eventDesc{
  border-radius: 4px;
  width: 100%;
  margin-top: 0.75em;
  margin-bottom: 0em;
  margin-left: -.2em;
}

form textarea{
  padding-top: 0.25em;
  padding-bottom: 0.25em;
  margin-top: 0.5em;
  margin-left: 1.75em;
}

div#user-profile form #router-link{
  color: white;
  padding-left: 1.4em;
  padding-top: 5em;
}

div#create-event form button {
  margin-top: 1.5em;
  padding: 0.5em;
  margin-left: auto;
  margin-right: auto;
  text-align: center
}

#buttonDiv{
  width: 100%;
  text-align: center;
}


</style>