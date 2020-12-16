<template>
  <div id="create-team">
    <h1>Create A Team</h1>
      <form class="form-createteam" @submit.prevent="createTeam">
      <label id="teamName" for="teamName" class="sr-only">Team Name: </label>
      <input
        type="text"
        id="teamName"
        class="form-control"
        placeholder="Team Name"
        v-model="team.TeamName"
        required
        autofocus
      />
      <br />

      <label>Drag and drop your Team image here:</label>
      <drop-zone id="dropzone"/>

      <div class="teamDescription">
      <label for="bio" class="sr-only">Team Bio: </label><br />
      <textarea
        type="text"
        id="team-bio"
        class="form-control"
        rows="8" 
        cols="40"
        placeholder="How will your team Akt?"
        v-model="team.TeamBio"
        required
      />
      </div>
      
      
      <label for="teamCity">City: </label>
      <input id="teamCity" type="text" placeholder="City" v-model="team.TeamCity" required />
      <br />

      <label for="teamState">State: </label>
      <input id="teamState" type="text" placeholder="State" v-model="team.TeamState" required />
      <br />
      <label for="teamZip" class="sr-only">Zip Code: </label>
      <input
        type="text"
        id="teamZip"
        class="form-control"
        placeholder="Zip Code"
        v-model="team.TeamZip"
        required
      />
      <br />
      <label for="e-mail">Lead Contact: </label>
      <input id="e-mail" type="email" placeholder="E-Mail Address" v-model="team.TeamContactEmail" required /> <br />

      <div id="causeList"> 
      <p id="causeSelect">Select which causes apply to this team:</p>
      <tr v-for="cause in causes" v-bind:key="cause.causeId">
        <td>
          <input type="checkbox" v-bind:id="cause.causeId" v-bind:value="cause.causeId" v-model="team.TeamCauses">
        </td>
        <td>
          {{cause.causeName}}
        </td>
      </tr>
      </div>

      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Team
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

div#create-team h1{
  color: white;
  margin-block: 0.5em;
  padding-left: 0.75em;
}

div#create-team{
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

form input#teamName {
  margin-top: 2.25em;
}

#dropzone{
  margin-left: 1.5em;
  margin-block: 0.5em;
}

textarea#team-bio{
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

.teamDescription * {
  vertical-align: top;
}

div#user-profile form #router-link{
  color: white;
  padding-left: 1.4em;
  padding-top: 5em;
}

div#create-team form button {
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