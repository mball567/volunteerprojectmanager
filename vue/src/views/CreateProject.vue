<template>
  <div id="create-project" class="container">
    <div id="createForm">
      <h1>Create Project</h1>
        <form class="form-createproject" @submit.prevent="createProject">
        <!-- <label id="projName" for="projName" class="sr-only">Project Name: </label> -->
        <input type="text" id="projName" class="form-control" placeholder="Project Name" v-model="project.ProjName" required autofocus /> <br/>

        <!-- <label>Drag and drop your Project image here:</label> -->
        <drop-zone id="dropzone"/>

        <!-- <div class="projectDescription"> -->
        <!-- <label for="projDesc" class="sr-only">Project Description: </label> <br/> -->
        <textarea type="text" id="projDesc" class="form-control" rows="8" cols="40" placeholder="Project Description" v-model="project.ProjDesc" required/>
        <!-- </div> -->
      
        <!-- <label for="projHours">Estimated hours for project completion: </label> -->
        <input id="projHours" type="number" placeholder="Estimated Hours To Complete Project" v-model="project.projWorkingHours" required />
      
      
        <!-- <label for="projCity">City: </label> -->
        <input id="projCity" type="text" placeholder="City" v-model="project.ProjCity" required />
      

        <!-- <label for="projState">State: </label> -->
        <input id="projState" type="text" placeholder="State" v-model="project.ProjState" required />
      

        <!-- <label for="projZip" class="sr-only">Zip Code: </label> -->
        <input type="text" id="projZip" class="form-control" placeholder="Zip Code" v-model="project.ProjZip" required />
      

        <!-- <label for="e-mail">Lead Contact E-Mail: </label> -->
        <input id="e-mail" type="email" placeholder="Lead Contact E-Mail Address" v-model="project.ProjContactEmail" required /> 

        <p id="causeSelect">Select which causes apply to this project:</p>
        <div id="causeList">         
        <div class="cause" v-for="cause in causes" v-bind:key="cause.causeId">        
          <input type="checkbox" v-bind:id="cause.causeId" v-bind:value="cause.causeId" v-model="project.ProjCauses"/>
          {{cause.causeName}}
        </div>
        </div>
        <div id="buttonDiv">
        <button class="btn btn-lg btn-primary btn-block" type="submit">Create Project</button>
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

div#create-project.container{
  display: grid;
  grid-template-columns: 375px 1fr 375px;
  grid-template-areas: 
  ". form .";
}

@media screen and (max-width: 1024px){
div#create-project.container{
  grid-template-columns: 200px 1fr 200px;
}
}

@media screen and (max-width: 768px){
div#create-project.container{
  grid-template-columns: 100px 1fr 100px;
}
}

div#createForm{
  grid-area: form;
}

div#create-project h1{
  color: white;
  margin-block: 0.5em;
  padding-left: 0.75em;
  text-align: center;
}

div#create-project{
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

div.cause{
  width: 225px;
}

input,
textarea,
select {
  font-family: 'Montserrat', sans-serif;
  font-size: 16px;
  color: #4c4c4c;
}

/* form label {
  color: white;
  padding-left: 1.4em;
  padding-right: 0.5em;
} */

/* form input#projName {
  margin-top: 2.25em;
} */

#dropzone{
    font-family: 'Montserrat', sans-serif;
    font-size: 16px;
    color: #4c4c4c;
    width: 100%;
    margin-top: 0.75em;
    border-radius: 4px;
}



textarea#projDesc{
  border-radius: 4px;
  width: 100%;
  margin-top: 0.75em;
  margin-bottom: 0em;
}

/* form input#e-mail {
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
} */

/* div#user-profile form #router-link{
  color: white;
  padding-left: 1.4em;
  padding-top: 5em;
} */

div#create-project form button {
  margin-top: 1.5em;
  padding: 0.5em;
  margin-left: auto;
  margin-right: auto;
  text-align: center;
}

#buttonDiv{
  width: 100%;
  text-align: center;
}

p#causeSelect{
  font-size: 1.5em;
  padding-left: 0.2em;
  font-weight: bold;
  width: 100%;
}

form div#causeList {
  color: white;
  padding-left: 1.25em;
  
}
</style>