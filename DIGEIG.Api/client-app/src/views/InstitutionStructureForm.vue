<template>
  <div class="d-flex align-items-center justify-content py-5">
    <b-container class="border w-50 p-4 shadow rounded mt-5">
      <div class="d-flex justify-content-start align-items-center w-100 pt-2">
        <h4>Formulario de Estructuras</h4>
      </div>
      <div class="mt-3">
        <b-row>
          <b-col>
            <b-form-group
              class="fs-6 text-left"
              label="Nombre de la estructura"
            >
              <b-form-input
                :class="[
                  !$v.Record.name.required ? 'border border-danger' : '',
                ]"
                placeholder="Nombre de la estructura"
                v-model.trim="Record.name"
              ></b-form-input>
              <p
                class="text-danger text-size-required m-0"
                v-if="!$v.Record.name.required"
              >
                Nombre de estructura.
              </p>
              <p
                class="text-danger text-size-required"
                v-if="!$v.Record.name.minLength"
              >
                La estructura debe tener minimo 10 caracteres.
              </p>
            </b-form-group>
          </b-col>

          <b-col v-if="validate">
            <b-form-group
              class="fs-6 text-left"
              label="Seleccionar estructura padre"
            >
              <b-form-select
                v-model="Record.mainInstitutionStructureId"
                :class="[msgInsitution ? 'border border-danger' : '']"
              >
                <option
                  v-for="institution in InstitutionList"
                  :key="institution.id"
                  :value="institution.institutionStructureId"
                >
                  {{ institution.name }}
                </option>
              </b-form-select>
              <p class="text-danger text-size-required" v-if="msgInsitution">
                Seleccionar estructura padre.
              </p>
            </b-form-group>
          </b-col>
        </b-row>
      </div>
      <div class="px-2">
        <b-row>
          <b-form-group
            class="text-left w-100 px-2 mb-3"
            label="Descripcion de la estructura"
          >
            <vue-editor
              v-model="Record.description"
              :class="[
                !$v.Record.description.required ? 'border border-danger' : '',
              ]"
            ></vue-editor>
            <!-- <b-form-textarea
              :class="[
                !$v.Record.description.required ? 'border border-danger' : '',
              ]"
              id="textarea-no-resize"
              placeholder="Descripcion de la estructura"
              rows="4"
              no-resize
              v-model.trim="Record.description"
            ></b-form-textarea> -->
            <p
              class="text-danger text-size-required"
              v-if="!$v.Record.description.required"
            >
              Descripcion de la estructura.
            </p>
            <p
              class="text-danger text-size-required"
              v-if="!$v.Record.description.minLength"
            >
              La descripcion de la estructura debe tener minimo 30 caracteres.
            </p>
          </b-form-group>
        </b-row>
      </div>
      <div class="w-100 d-flex justify-content-between">
        <b-col cols="4" class="border p-0">
          <b-button
            class="w-100"
            variant="primary"
            @click="
              $router.push({
                name: 'InstitutionStructure',
                params: { id: $route.params.id },
              })
            "
          >
            Atras
          </b-button>
        </b-col>
        <b-col cols="4" class="border p-0">
          <b-button
            class="w-100"
            variant="success"
            @click="ActionRecord()"
            :disabled="this.$v.$invalid"
          >
            Guardar
          </b-button>
        </b-col>
      </div>
    </b-container>
  </div>
</template>

<script>
import { required, minLength } from "vuelidate/lib/validators";
import {
  getInstitutionRecords,
  getInstitutionRecordById,
  postInstitutionStructure,
  putInstitutionStructure,
} from "../api/Institution/InstitutionStructureService";
import * as names from "../api/names";
import { VueEditor } from "vue2-editor";
export default {
  name: "InstitutionStructureForm",
  components: {
    VueEditor,
  },
  data() {
    return {
      Record: {
        id: 0,
        institutionStructureId: 0,
        institutionId: 0,
        name: "",
        description: "",
        mainInstitutionStructureId: null,
      },
      InstitutionList: [],
      validate: false,
      msgInsitution: false,
      content: "<h1>Some initial content</h1>",
    };
  },
  validations: {
    Record: {
      name: {
        required,
        minLength: minLength(10),
      },
      description: {
        required,
        minLength: minLength(30),
      },
    },
  },
  created() {
    this.Record.institutionId = this.$route.params.id;
    this.Record.id = this.$route.params.idRecord;
    if (this.Record.id.length >= 1) this.getStructureInstitutionRecordById();
    this.getInstitutionRecords();
  },
  methods: {
    clearForm() {
      for (const key in this.Record) {
        this.Record[key] = null;
      }
      this.Record.institutionId = this.$route.params.id;
      this.Record.mainInstitutionStructureId = 0;
    },
    async getInstitutionRecords() {
      const response = await getInstitutionRecords(this.Record.institutionId);
      this.InstitutionList = response.data;
      this.validate = false;
      if (
        this.InstitutionList.length > 0 &&
        this.Record.mainInstitutionStructureId !== 0
      ) {
        this.validate = true;
      }
    },
    async getStructureInstitutionRecordById() {
      const response = await getInstitutionRecordById(this.Record.id);
      this.validate = false;
      this.Record = response.data;
      if (
        this.InstitutionList.length > 0 &&
        this.Record.mainInstitutionStructureId !== 0
      ) {
        this.validate = true;
      }
    },
    async save() {
      let methodRecord;
      if (this.Record.id) {
        this.$swal
          .fire({
            title: "Estas seguro de que quieres editar el registro?",
            icon: "warning",
            showCancelButton: true,
            cancelButtonColor: "#FF0017",
            confirmButtonText: "Aceptar",
            confirmButtonColor: "#003876",
          })
          .then(async (result) => {
            if (result.isConfirmed) {
              try {
                methodRecord = putInstitutionStructure;
                // let url = `${names.API_ENDPOINT_INSTITUTION_STRUCTURE}`;
                // fetch(url, {
                //   method: "PUT",
                //   headers: {
                //     "Content-Type": "application/json",
                //   },
                //   body: JSON.stringify(this.Record),
                // })
                //   .then((response) => {
                //     return response.json();
                //   })
                //   .then((dataOne) => console.log(dataOne))
                //   .catch((error) => console.log("error", error));
                const response = await methodRecord(this.Record);
                this.$swal.fire({
                  title: "El registro ha sido editado correctamente!",
                  icon: "success",
                  confirmButtonText: "Aceptar",
                  confirmButtonColor: "#003876",
                });
                return response;
              } catch (error) {
                const errorList = error.response.data.Response.errors;
                this.$swal.fire({
                  icon: "error",
                  html: `<ul>${errorList.map(
                    (error) => "<li>" + error.errorMessage + "</li>"
                  )}</ul>`,
                  title: "Ha ocurrido un inconveniente",
                  confirmButtonText: "Aceptar",
                  confirmButtonColor: "#003876",
                });
              }
            }
          });
      } else {
        try {
          methodRecord = postInstitutionStructure;
          const response = await methodRecord(this.Record);
          this.$swal
            .fire({
              icon: "success",
              title: "Registro guardado exitosamente",
              confirmButtonText: "Aceptar",
              confirmButtonColor: "#003876",
            })
            .then((result) => {
              if (result.isConfirmed) {
                this.$router.push({
                  name: "InstitutionStructure",
                  params: { id: this.Record.institutionId },
                });
                this.clearForm();
              } else {
                return;
              }
            });
          return response;
        } catch (error) {
          const errorList = error.response.data.Response.errors;
          this.$swal.fire({
            icon: "error",
            title: "Ha ocurrido un inconveniente",
            html: `<ul>${errorList.map(
              (error) => "<li>" + error.errorMessage + "</li>"
            )}</ul>`,
            confirmButtonText: "Aceptar",
            confirmButtonColor: "#003876",
          });
        }
      }
    },
    ActionRecord() {
      if (this.InstitutionList.length === 0) {
        this.Record.mainInstitutionStructureId = 0;
        this.save();
      } else {
        if (
          this.InstitutionList.length !== 0 &&
          this.Record.mainInstitutionStructureId === null
        ) {
          this.msgInsitution = true;
          return;
        } else {
          this.save();
        }
      }
    },
  },
};
</script>

<style>
.text-size-required {
  font-size: 12px;
}
</style>
