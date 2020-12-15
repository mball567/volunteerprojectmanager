<template>
  <div>
      <input type="checkbox" name="searchOrganization" id="searchOrganization" v-model="request.searchOrganization">

        <h1>Search for an Organization</h1>
      <form class="search" @submit.prevent="search">
      <label id="orgName" for="orgName" class="sr-only">Organization Name: </label>
      <input
        type="text"
        id="orgName"
        class="form-control"
        placeholder="Organization Name"
        v-model="request.name"
        autofocus
      />

      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Search
      </button>
    </form>

    <div v-if="searchResult.organizations">
        <h1>Organizations</h1>
        <p v-for="org in searchResult.organizations" v-bind:key="org.orgId">{{org.orgName}}</p>
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
            alert(this.request.name);
            WebService.search(this.request)
            .then ( (response) => {
                this.searchResult = response.data;
            })
        }
    }
}
</script>

<style>

</style>