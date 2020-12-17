<template>
  <div id="create-team" class="container">
    <div id="createForm">
    <h1>Create Team</h1>
      <form class="form-createteam" @submit.prevent="createTeam">
      <!-- <label id="teamName" for="teamName" class="sr-only">Team Name: </label> -->
      <input type="text" id="teamName" class="form-control" placeholder="Team Name" v-model="team.TeamName" required autofocus />     

      <!-- <label>Drag and drop your Team image here:</label> -->
      <drop-zone id="dropzone"/>

      <!-- <div class="teamDescription"> -->
      <!-- <label for="bio" class="sr-only">Team Bio: </label><br />  -->
      <textarea
        type="text"
        id="team-bio"
        class="form-control"
        rows="8" 
        cols="40"
        placeholder="How will your team Akt?"
        v-model="team.TeamBio"
        required/>
      <!-- </div> -->
      
      <!-- <label for="teamCity">City: </label> -->
      <input id="teamCity" type="text" placeholder="City" v-model="team.TeamCity" required />
    

      <!-- <label for="teamState">State: </label> -->
      <input id="teamState" type="text" placeholder="State" v-model="team.TeamState" required />
  
      <!-- <label for="teamZip" class="sr-only">Zip Code: </label> -->
      <input
        type="text"
        id="teamZip"
        class="form-control"
        placeholder="Zip Code"
        v-model="team.TeamZip"
        required
      />

      <!-- <label for="e-mail">Lead Contact: </label> -->
      <input id="e-mail" type="email" placeholder="E-Mail Address" v-model="team.TeamContactEmail" required /> 

      <p id="causeSelect">Select all causes that apply to this team:</p>
      <div id="causeList"> 
      <div class="cause" v-for="cause in causes" v-bind:key="cause.causeId">
          <input type="checkbox" v-bind:id="cause.causeId" v-bind:value="cause.causeId" v-model="team.TeamCauses"/>
          {{cause.causeName}}
      </div>
      </div>

      <div id="buttondiv"> 
        <button class="btn btn-lg btn-primary btn-block" type="submit">Create Team</button>
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
            team: {
                CreatedBy: 0,
                TeamName: "",
                TeamImage: "",
                TeamBio: "",
                TeamZip: "",
                TeamCity: "",
                TeamState: "",
                TeamContactEmail: "",
                TeamCauses: [],
            },
            causes: [],
        };
    },
    methods: {
        createTeam(){
          this.team.CreatedBy = this.$store.state.user.profile.profileId;
          this.team.TeamImage= this.$store.state.userImage;

          WebService  
          .createTeam(this.team)
          .then((response) => {
            if (response.status == 201) {
              this.$store.state.userImage = "";
              this.$router.push({
                path: "/profiles/my",
              });
            }
          })
        }
    },
        created() {
      this.causes = this.$store.state.causes;
    },
}
</script>

<style scoped>

div#create-team.container{
  display: grid;
  grid-template-columns: 375px 1fr 375px;
  grid-template-areas: 
  ". form .";
}

@media screen and (max-width: 1024px){
div#create-team.container{
  grid-template-columns: 200px 1fr 200px;
}
}

@media screen and (max-width: 768px){
div#create-team.container{
  grid-template-columns: 100px 1fr 100px;
}
}
  
div#createForm {
  grid-area: form;
}

div#create-team h1{
  color: white;
  margin-block: 0.5em;
  padding-left: 0.75em;
  text-align: center;
}

div#create-team{
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
}

div.cause {
  width: 225px;
}

input,
textarea,
select {
  font-family: 'Montserrat', sans-serif;
  font-size: 16px;
  color: #4c4c4c;
}

#dropzone{
  font-size: 16px;
  color: #4c4c4c;
  width: 100%;
  margin-top: 0.75em;
  font-family: 'Montserrat', sans-serif;
  border-radius: 4px;
}

textarea#team-bio{
  width: 100%;
  margin-bottom: 0.5em;
  margin-top: 0.75em;
  border-radius: 4px; 
  box-shadow: 1px 2px 5px rgba(0,0,0,.09); 
}

div#create-team form button{
  margin-top: 1.5em;
  padding: 0.5em;
  margin-left: auto;
  margin-right: auto;
  text-align: center;
}

#buttondiv {
  width: 100%;
  text-align: center;
}

p#causeSelect{
  font-size: 1.5em;
  padding-left: 0.2em;
  font-weight: bold;
  width: 100%;

}

form div#causeList{
  color: white;
  padding-left: 1.25em;
}

</style>