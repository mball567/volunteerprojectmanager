<template>
  <div id="searchPage">
      <label id="searchOrgLabel" for="searchOrganization" class="sr-only">Search By Organization Name: </label>
      <input type="checkbox" name="searchOrganization" id="searchOrganization" v-model="request.searchOrganization"> <br/>

      <label id="searchProjectLabel" for="searchProject" class="sr-only">Search By Project Name: </label>
      <input type="checkbox" name="searchProject" id="searchProject" v-model="request.searchProject"><br/>

      <label id="searchTeamLabel" for="searchTeam" class="sr-only">Search By Team Name: </label>
      <input type="checkbox" name="searchTeam" id="searchTeam" v-model="request.searchTeam"><br/>

      <label id="searchProfileLabel" for="searchProfile" class="sr-only">Search By Profile Name: </label>
      <input type="checkbox" name="searchProfile" id="searchProfile" v-model="request.searchProfile"><br/>

      <h1>Search</h1>
      <form class="search" @submit.prevent="search">
      <label id="nameSearchLabel" for="nameSearch" class="sr-only">Search By Name: </label>
      <input
        type="text"
        id="nameSearch"
        class="form-control"
        placeholder="Type Search Term Here"
        v-model="request.name"
        autofocus
      />

      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Search
      </button>
    </form>

    <div v-if="searchResult.organizations">
      <h1 v-if="searchResult.organizations.length > 0">Organizations</h1>
      <p class="link" v-for="org in searchResult.organizations" v-bind:key="org.orgId">{{org.orgName}}</p>
    </div>

    <div v-if="searchResult.profiles">
      <h1 v-if="searchResult.profiles.length > 0">Profiles</h1>
      <p class="link" v-for="profile in searchResult.profiles" v-bind:key="profile.profileId">{{profile.firstName}} {{profile.lastName}}</p>
    </div>

    <div v-if="searchResult.teams">
      <h1 v-if="searchResult.teams.length > 0">Teams</h1>
      <p class="link" v-for="team in searchResult.teams" v-bind:key="team.teamId" v-on:click="$router.push({path:`/teams/${team.teamId}`})">{{team.teamName}}</p>
    </div>

    <div v-if="searchResult.projects">
      <h1 v-if="searchResult.projects.length > 0">Projects</h1>
      <p class="link" v-for="proj in searchResult.projects" v-bind:key="proj.projId" v-on:click="$router.push({path:`/projects/${proj.projId}`})">{{proj.projName}}</p>
    </div>
  </div>
</template>

<script>
import WebService from '../services/WebService.js'

export default {
    name: 'search',
    data() {
        return {
            request: {
                searchOrganization: false,
                searchProject: false,
                searchProfile: false,
                searchTeam: false,
                name: "",
            },
            searchResult: {},
        };
    },
    methods: {
        search(){
            WebService.search(this.request)
            .then ( (response) => {
                this.searchResult = response.data;
            })
        }
    }
}
</script>

<style>
#searchPage {
  color: white;
}
.link:hover{
color: #ffb703ff;
cursor: pointer;
font-weight: bold;
text-decoration: underline;
}
</style>