<template>
  <div id="create-project">
    <h1>Create Project</h1>
      <form class="form-createproject" @submit.prevent="createProject">
      <label id="projName" for="projName" class="sr-only">Project Name: </label>
      <input
        type="text"
        id="projName"
        class="form-control"
        placeholder="Project Name"
        v-model="project.ProjName"
        required
        autofocus
      /> <br/>

      <label>Drag and drop your Project image here:</label>
      <drop-zone />

      <br />
      <div class="projectDescription">
      <label for="projDesc" class="sr-only">Project Description: </label> <br/>
      <textarea
        type="text"
        id="projDesc"
        class="form-control"
        rows="8" 
        cols="40"
        placeholder="How will this project akt?"
        v-model="project.ProjDesc"
        required
      />
      </div>
      <br />

      <label for="projHours">How many hours do estimate this project will take: </label>
      <input id="projHours" type="number" placeholder="Hours To Complete" v-model="project.projWorkingHours" required />
      <br />
      
      <label for="projCity">City: </label>
      <input id="projCity" type="text" placeholder="City" v-model="project.ProjCity" required />
      <br />

      <label for="projState">State: </label>
      <input id="projState" type="text" placeholder="State" v-model="project.ProjState" required />
      <br />

      <label for="projZip" class="sr-only">Zip Code: </label>
      <input
        type="text"
        id="projZip"
        class="form-control"
        placeholder="Zip Code"
        v-model="project.ProjZip"
        required
      />
      <br />

      <label for="e-mail">Lead Contact E-Mail: </label>
      <input id="e-mail" type="email" placeholder="E-Mail Address" v-model="project.ProjContactEmail" required /> <br />

      <div id="causeList"> 
      <p>Select which causes apply to this project:</p>
      <tr v-for="cause in causes" v-bind:key="cause.causeId">
        <td>
          <input type="checkbox" v-bind:id="cause.causeId" v-bind:value="cause.causeId" v-model="project.ProjCauses">
        </td>
        <td>
          {{cause.causeName}}
        </td>
      </tr>
      </div>

      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Project
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
            project: {
                UserId: 0,
                ProfId: 0,
                ProjName: "",
                ProjDesc: "",
                ProjImage: "",
                ProjZip: 0,
                ProjCity: "",
                ProjState: "",
                ProjWorkingHours: 0,
                ProjContactEmail: "",
                ProjCauses: [],
            },
            causes: [],
        };
    },
    methods: {
        createProject(){
          if(this.$store.state.user.profile){
              this.project.ProfId = this.$store.state.user.profile.profileId;
          }
          this.project.UserId = this.$store.state.user.userId;
          this.project.ProjImage = this.$store.state.userImage;

          WebService  
          .createProject(this.project)
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

div#create-project h1{
  color: white;
  margin-block: 0.5em;
  padding-left: 0.75em;
}

div#create-project{
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

form input#projName {
  margin-top: 2.25em;
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

.projectDescription * {
  vertical-align: top;
}

div#user-profile form #router-link{
  color: white;
  padding-left: 1.4em;
  padding-top: 5em;
}

div#create-project form button {
  margin-top: 0.5em;
  padding: 0.5em;
  margin-left: 1.5em;
}

form div#causeList {
  color: white;
}
</style>