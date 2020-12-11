<template>
  <div>
      <form class="form-createteam" @submit.prevent="createTeam">
      <label for="teamName" class="sr-only">Team Name: </label>
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
      <label for="bio" class="sr-only">Bio: </label>
      <input
        type="text"
        id="bio"
        class="form-control"
        placeholder="Bio"
        v-model="team.TeamBio"
        required
      />
      <br />
      <label for="teamZip" class="sr-only">Zip: </label>
      <input
        type="text"
        id="teamZip"
        class="form-control"
        placeholder="Zip Code"
        v-model="team.TeamZip"
        required
      />
      <br />
      <label for="teamCity">Team City: </label>
      <input id="teamCity" type="text" v-model="team.TeamCity" required />
      <br />

      <label for="teamState">Team State: </label>
      <input id="teamState" type="text" v-model="team.TeamState" required />
      <br />

      <label for="e-mail">E-Mail Address: </label>
      <input type="email" v-model="team.TeamContactEmail" required /> <br />

      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Team
      </button>
    </form>
  </div>
</template>

<script>
import WebService from '../services/WebService.js'
export default {
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
            },
        };
    },
    methods: {
        createTeam(){
          this.team.CreatedBy = this.$store.state.user.profile.profileId;
          WebService  
          .createTeam(this.team)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/profiles/my",
              });
            }
          })
        }
    }
}
</script>

<style>

</style>