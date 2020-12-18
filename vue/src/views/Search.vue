<template>
  <div id="searchPage" class="container">
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

      <div id="search">
      <h1>Search</h1>

      <p>Select the following item(s) you would like to search for:</p>
      <label id="searchOrgLabel" for="searchOrganization" class="sr-only">Search By Organization: </label>
      <input type="checkbox" name="searchOrganization" id="searchOrganization" v-model="request.searchOrganization"> <br/>

      <label id="searchProjectLabel" for="searchProject" class="sr-only">Search By Project: </label>
      <input type="checkbox" name="searchProject" id="searchProject" v-model="request.searchProject"><br/>

      <label id="searchTeamLabel" for="searchTeam" class="sr-only">Search By Team: </label>
      <input type="checkbox" name="searchTeam" id="searchTeam" v-model="request.searchTeam"><br/>

      <label id="searchProfileLabel" for="searchProfile" class="sr-only">Search By Profile: </label>
      <input type="checkbox" name="searchProfile" id="searchProfile" v-model="request.searchProfile"><br/>

      <p>Input keywords here if you would like to search the selected item(s) by name:</p>
      <form class="search" @submit.prevent="search">
      <label id="nameSearchLabel" for="nameSearch" class="sr-only">Search By Name: </label>
      <input
        type="text"
        id="nameSearch"
        class="form-control"
        placeholder="Type Search Term Here"
        v-model="request.name"
        autofocus
        :disabled="request.searchCause"
      /> <br/>

      <p>Check this box if you would like to search the selected item(s) by causes instead of name:</p>
      <label id="searchCauseLabel" for="searchCause" class="sr-only">Search By Causes: </label>
      <input type="checkbox" name="searchCause" id="searchCause" v-model="request.searchCause" @change="request.name =''"><br/>
      <div id="causeList" v-if="request.searchCause"> 
      <p>Select all causes you would like to search by:</p>
      <tr v-for="cause in causes" v-bind:key="cause.causeId">
        <td>
          <input type="checkbox" v-bind:id="cause.causeId" v-bind:value="cause.causeId" v-model="request.causeIds">
        </td>
        <td>
          {{cause.causeName}}
        </td>
      </tr>
      </div>

      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Search
      </button>
    </form>

    <div v-if="searchResult.organizations">
      <h1 v-if="searchResult.organizations.length > 0">Organizations</h1>
      <p class="link" v-for="org in searchResult.organizations" v-bind:key="org.orgId" v-on:click="$router.push({path:`/profiles/${org.userId}`})">{{org.orgName}}</p>
    </div>

    <div v-if="searchResult.profiles">
      <h1 v-if="searchResult.profiles.length > 0">Profiles</h1>
      <p class="link" v-for="profile in searchResult.profiles" v-bind:key="profile.profileId" v-on:click="$router.push({path:`/profiles/${profile.userId}`})">{{profile.firstName}} {{profile.lastName}}</p>
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
    </main>
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
                searchCause: false,
                name: "",
                causeIds: [],
            },
            searchResult: {},
            causes: [],
        };
    },
    methods: {
        search(){
            WebService.search(this.request)
            .then ( (response) => {
                this.searchResult = response.data;
            })
        }
    },
    created(){
      this.causes = this.$store.state.causes;
    },
}
</script>

<style scoped>
#main-grid {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas:
    "nav nav nav"
    ". search .";
}

#searchPage {
  color: white;
}
.link:hover{
color: #ffb703ff;
cursor: pointer;
font-weight: bold;
text-decoration: underline;
}

main nav#site-nav {
  grid-area: nav;
  width: 100%;
  text-align: right;
  background: white;
}

main nav#site-nav ul {
  display: flex;
  align-items: stretch;
  justify-content: space-evenly;
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

div#search{
  grid-area: search;
}
</style>