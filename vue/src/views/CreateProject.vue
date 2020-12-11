<template>
  <div>
      <form class="form-createproject" @submit.prevent="createProject">
      <label for="projName" class="sr-only">Project Name: </label>
      <input
        type="text"
        id="projName"
        class="form-control"
        placeholder="Project Name"
        v-model="project.ProjName"
        required
        autofocus
      />
      <br />
      <label for="projDesc" class="sr-only">Project Description: </label>
      <input
        type="text"
        id="projDesc"
        class="form-control"
        placeholder="Project Description"
        v-model="project.ProjDesc"
        required
      />
      <br />
      <label for="projZip" class="sr-only">Project Zip Code: </label>
      <input
        type="text"
        id="projZip"
        class="form-control"
        placeholder="Zip Code"
        v-model="project.ProjZip"
        required
      />
      <br />
      <label for="projCity">Project City: </label>
      <input id="projCity" type="text" v-model="project.ProjCity" required />
      <br />

      <label for="projState">Project State: </label>
      <input id="projState" type="text" v-model="project.ProjState" required />
      <br />

      <label for="e-mail">Project Contact E-Mail Address: </label>
      <input type="email" v-model="project.ProjContactEmail" required /> <br />

      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Project
      </button>
    </form>
  </div>
</template>

<script>
import WebService from '../services/WebService.js'
export default {
    data() {
        return{
            project: {
                OrgId: 0,
                ProfId: 0,
                ProjName: "",
                ProjDesc: "",
                ProjImage: "",
                ProjZip: 0,
                ProjCity: "",
                ProjState: "",
                ProjWorkingHours: 0,
                ProjContactEmail: "",
            },
        };
    },
    methods: {
        createProject(){
          if(this.$store.state.user.organization){
              this.project.OrgId = this.$store.state.user.organization.orgId;
          }
          else{
              this.project.ProfId = this.$store.state.user.profile.profileId;
          }

          WebService  
          .createProject(this.project)
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