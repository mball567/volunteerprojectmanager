<template>
  <div>
  <body class="container">  
    <div class="testbox">
        <h1>Create A Blank</h1>
        <form action="/">
        <input type="text" name="username" id="firstName" placeholder="Username" required/>
        <input type="password" name="name" id="name" placeholder="Password" required/>
        <input type="password" name="name" id="name" placeholder="Confirm Password" required/>
        <input type="text" name="name" id="firstName" placeholder="First Name" required/>
        <input type="text" name="name" id="lastName" placeholder="Last Name" required/>
        <drop-zone id="dropzone"/>
        <input type="text" name="name" id="lastName" placeholder="User Bio" required/>
        <input type="text" name="name" id="lastName" placeholder="City" required/>
        <input type="text" name="name" id="lastName" placeholder="State" required/>
        <input type="text" name="name" id="lastName" placeholder="Zip Code" required/>
        <input type="text" name="name" id="name" placeholder="E-Mail Address" required/>
  
        <div id="causeList"> 
            <p>Select all causes in which you are interested:</p>
            <tr v-for="cause in causes" v-bind:key="cause.causeId">
            <td>
              <input type="checkbox" v-bind:id="cause.causeId" v-bind:value="cause.causeId" v-model="user.organization.OrgCauses">
            </td>
            <td>
              {{cause.causeName}}
            </td>
           </tr>
        </div>

        <a href="#" class="button">Register</a>
        </form>
    </div>
  </body>
</div>
</template>

<script>
import DropZone from '../components/DropZone.vue';



export default {
  name: 'create-template',
  components: { DropZone },
  data(){
      return{
          causes: [],
      }
  },
  methods: {
      created(){
          this.causes = this.$store.state.causes;
      }
  }

}
</script>

<style scoped>

@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap');
@import url('//netdna.bootstrapcdn.com/font-awesome/3.1.1/css/font-awesome.css');

.container{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-template-areas: 
    ". testbox ."
}

div, dl, dt, dd, ul, ol, li, h1, h2, h3, h4, h5, h6, 
pre, form, fieldset, input, textarea, p, blockquote, th, td { 
  padding:0;
  margin:0;}

fieldset, img {border:0}

ol, ul, li {list-style:none}

:focus {outline:none}

input,
textarea,
select {
  font-family: 'Montserrat', sans-serif;
  font-size: 16px;
  color: #4c4c4c;
}

p {
  font-size: 12px;
  width: 150px;
  display: inline-block;
  margin-left: 18px;
}
.testbox h1 {
  font-size: 32px;
  font-weight: 300;
  color: #4c4c4c;
  text-align: center;
  padding-top: 10px;
  margin-bottom: 10px;
}

html{
  background-color: #ffffff;
}

.testbox {
  grid-area: testbox;
  margin: 20px auto;
  width: 100%; 
  height: 100vh; 
  -webkit-border-radius: 8px/7px; 
  -moz-border-radius: 8px/7px; 
  border-radius: 8px/7px; 
  background-color: #ebebeb; 
  -webkit-box-shadow: 1px 2px 5px rgba(0,0,0,.31); 
  -moz-box-shadow: 1px 2px 5px rgba(0,0,0,.31); 
  box-shadow: 1px 2px 5px rgba(0,0,0,.31); 
  border: solid 1px #cbc9c9;
}

#dropzone{
    font-family: 'Montserrat', sans-serif;
    font-size: 16px;
    color: #4c4c4c;
    width: 100%;
    margin-top: 0.75em;
}



form{
  margin: 0 30px;
}

label.radio {
	cursor: pointer;
  text-indent: 35px;
  overflow: visible;
  display: inline-block;
  position: relative;
  margin-bottom: 15px;
}

label.radio:before {
  background: #3a57af;
  content:'';
  position: absolute;
  top:2px;
  left: 0;
  width: 20px;
  height: 20px;
  border-radius: 100%;
}

label.radio:after {
	opacity: 0;
	content: '';
	position: absolute;
	width: 0.5em;
	height: 0.25em;
	background: transparent;
	top: 7.5px;
	left: 4.5px;
	border: 3px solid #ffffff;
	border-top: none;
	border-right: none;

	-webkit-transform: rotate(-45deg);
	-moz-transform: rotate(-45deg);
	-o-transform: rotate(-45deg);
	-ms-transform: rotate(-45deg);
	transform: rotate(-45deg);
}

input[type=radio]:checked + label:after {
	opacity: 1;
}

hr{
  color: #a9a9a9;
  opacity: 0.3;
}

input{
  width: 100%;
  height: 40px;
  -webkit-border-radius: 0px 4px 4px 0px/5px 5px 4px 4px; 
  -moz-border-radius: 0px 4px 4px 0px/0px 0px 4px 4px; 
  border-radius: 0px 4px 4px 0px/5px 5px 4px 4px; 
  background-color: #fff; 
  -webkit-box-shadow: 1px 2px 5px rgba(0,0,0,.09); 
  -moz-box-shadow: 1px 2px 5px rgba(0,0,0,.09); 
  box-shadow: 1px 2px 5px rgba(0,0,0,.09); 
  border: solid 1px #cbc9c9;
  margin-left: -5px;
  margin-top: 0.75em; 
  padding-left: 10px;
}





#icon {
  display: inline-block;
  width: 30px;
  background-color: #023047;
  padding: 8px 0px 8px 15px;
  margin-left: 15px;
  -webkit-border-radius: 4px 0px 0px 4px; 
  -moz-border-radius: 4px 0px 0px 4px; 
  border-radius: 4px 0px 0px 4px;
  color: white;
  -webkit-box-shadow: 1px 2px 5px rgba(0,0,0,.09);
  -moz-box-shadow: 1px 2px 5px rgba(0,0,0,.09); 
  box-shadow: 1px 2px 5px rgba(0,0,0,.09); 
  border: solid 0px #cbc9c9;
}

.gender {
  margin-left: 30px;
  margin-bottom: 30px;
}

.accounttype{
  margin-left: 8px;
  margin-top: 20px;
}

a.button {
  font-size: 14px;
  font-weight: 600;
  color: white;
  padding: 6px 25px 0px 20px;
  margin: 10px 8px 20px 0px;
  display: inline-block;
  float: right;
  text-decoration: none;
  width: 50px; height: 27px; 
  -webkit-border-radius: 5px; 
  -moz-border-radius: 5px; 
  border-radius: 5px; 
  background-color: #3a57af; 
  -webkit-box-shadow: 0 3px rgba(58,87,175,.75); 
  -moz-box-shadow: 0 3px rgba(58,87,175,.75); 
  box-shadow: 0 3px rgba(58,87,175,.75);
  transition: all 0.1s linear 0s; 
  top: 0px;
  position: relative;
}

a.button:hover {
  top: 3px;
  background-color:#2e458b;
  -webkit-box-shadow: none; 
  -moz-box-shadow: none; 
  box-shadow: none;
  
}
</style>