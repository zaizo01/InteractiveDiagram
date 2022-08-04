<template>
  <div class="d-flex p-5">
    <div class="container my-5">
      <b-row class="d-flex justify-content w-75 mt-2 ml-1" align-h="between">
        <b-col cols="2" class="pl-0">
          <b-form-group
            label="Por pagina"
            label-align="left"
            label-for="per-page-select"
            label-size="sm"
            class="mb-0"
          >
            <b-form-select
              id="per-page-select"
              v-model="perPage"
              :options="pageOptions"
              size="sm"
            ></b-form-select>
          </b-form-group>
        </b-col>

        <b-col cols="6" class="pr-0">
          <b-form-group
            label="Buscar"
            label-align="left"
            label-for="filter-input"
            label-size="sm"
            class="mb-0"
          >
            <b-input-group size="sm">
              <b-form-input
                id="filter-input"
                v-model="search"
                type="search"
                placeholder="Estructura"
              ></b-form-input>

              <b-input-group-append>
                <b-button
                  :disabled="!search"
                  @click="search = ''"
                  variant="primary"
                >
                  Limpiar
                </b-button>
              </b-input-group-append>
            </b-input-group>
          </b-form-group>
        </b-col>
      </b-row>
      <div class="w-75 mt-4">
        <div
          class="border d-flex align-items-center justify-content-between p-3 bg-white text-dark"
        >
          <div>
            <h4 class="m-0">Estructuras</h4>
          </div>
          <div>
            <b-button
              size="sm"
              variant="primary"
              @click="
                $router.push({
                  name: 'InstitutionStructureForm',
                  params: { id: institutionId, idRecord: 0 },
                })
              "
            >
              Agregar estructura
              <b-icon icon="plus-circle-fill" class="ml-2"></b-icon>
            </b-button>
            <b-button
              size="sm"
              class="ml-2"
              variant="primary"
              :disabled="!institutionRecords.length > 0"
              @click="
                $router.push({ name: 'Diagram', params: { id: institutionId } })
              "
            >
              Diagrama
              <b-icon icon="diagram2-fill"></b-icon>
            </b-button>
          </div>
        </div>
        <b-table
          striped
          hover
          :items="filteredList"
          :fields="fields"
          :current-page="currentPage"
          :per-page="perPage"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
          :sort-direction="sortDirection"
          :foot-clone="false"
          empty-text="No hay instituciones registradas"
          empty-filtered-text="No hay instituciones que coincidan con su solicitud"
          stacked="md"
          show-empty
          small
          class="mb-0 border"
        >
          <template #cell(index)="data">
            {{ data.index + 1 }}
          </template>

          <template #cell(name)="row">
            {{ row.item.name }}
            <b-icon
              icon="star-fill"
              variant="warning"
              v-if="row.item.mainInstitutionStructureId === 0"
            ></b-icon>
          </template>

          <template #cell(institution)="row">
            {{ row.item.mainInstitutionStructureId === 0 ? row.item.name : "" }}
          </template>

          <template #cell(actions)="row">
            <b-button
              variant="warning"
              size="sm"
              class="mx-2"
              @click="
                $router.push({
                  name: 'InstitutionStructureForm',
                  params: { id: institutionId, idRecord: row.item.id },
                })
              "
            >
              <b-icon
                icon="pencil-fill"
                aria-hidden="true"
                class="text-white"
              ></b-icon>
            </b-button>
            <b-button
              variant="danger"
              size="sm"
              @click="
                showAlertDeleteRecord(
                  row.item.id,
                  userEmail,
                  row.item.mainInstitutionStructureId
                )
              "
            >
              <b-icon icon="trash-fill" aria-hidden="true"></b-icon>
            </b-button>
          </template>
        </b-table>
        <div class="w-100 d-flex justify-content-end">
          <b-col sm="7" md="3" class="pr-0 mt-1">
            <b-pagination
              v-model="currentPage"
              :total-rows="totalRows"
              :per-page="perPage"
              align="fill"
              size="sm"
              class="my-0"
            ></b-pagination>
          </b-col>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {
  getInstitutionRecords,
  deleteInstitutionStructure,
} from "../api/Institution/InstitutionStructureService";
import * as names from "../api/names";
export default {
  name: "InstitutionStructure",
  data() {
    return {
      institutionId: null,
      search: '',
      institutionRecords: [],
      userEmail: "admin@gmail.com",
      fields: [
        "index",
        {
          key: "name",
          label: "Nombre",
          sortable: true,
          sortDirection: "desc",
          class: "text-left px-5",
        },
        { key: "actions", label: "Acciones" },
        {
          formatter: (value, key, item) => {
            return value ? "Yes" : "No";
          },
          sortable: true,
          sortByFormatted: true,
          filterByFormatted: true,
        },
      ],
      totalRows: 1,
      currentPage: 1,
      perPage: 5,
      pageOptions: [5, 10, 15, { value: 100, text: "Mostrar todos" }],
      sortBy: "",
      sortDesc: false,
      sortDirection: "asc",
    };
  },
  created() {
    this.getInstitutionRecords();
    this.institutionId = this.$route.params.id;
  },
  computed: {
    sortOptions() {
      return this.fields
        .filter((f) => f.sortable)
        .map((f) => {
          return { text: f.label, value: f.key };
        });
    },
    filteredList() {
      return this.institutionRecords.filter(record => {
        return record.name.toLowerCase().includes(this.search.toLowerCase())
      })
    }
  },
  mounted() {
    this.totalRows = this.institutionRecords.length;
  },
  methods: {
    async getInstitutionRecords() {
      const institutionId = this.$route.params.id;
      const response = await getInstitutionRecords(institutionId);
      this.institutionRecords = response.data;
      return response;
    },
    showAlertDeleteRecord(
      institutionId,
      userEmail,
      mainInstitutionStructureId
    ) {
      if (mainInstitutionStructureId === 0) {
        this.$swal.fire({
          title: "Oops...",
          text: "No puedes eliminar la estructura principal",
          icon: "error",
          confirmButtonText: "Aceptar",
          confirmButtonColor: "#003876",
        });
        return;
      } else {
        this.$swal
          .fire({
            title: "Estas seguro de que quieres eliminar el registro?",
            icon: "warning",
            showCancelButton: true,
            cancelButtonColor: "#FF0017",
            confirmButtonText: "Aceptar",
            confirmButtonColor: "#003876",
          })
          .then(async (result) => {
            if (result.isConfirmed) {
              try {
                const response = await deleteInstitutionStructure(
                  institutionId,
                  userEmail
                );
                // let url = `${names.API_ENDPOINT_INSTITUTION_STRUCTURE}/${institutionId}/${userEmail}`;
                // fetch(url, {
                //   method: "DELETE",
                // })
                //   .then((response) => {
                //     return response.json();
                //   })
                //   .then((data) => config.log(data));
                this.getInstitutionRecords();
                this.$swal.fire({
                  title: "El registro ha sido eliminado correctamente!",
                  icon: "success",
                  confirmButtonText: "Aceptar",
                  confirmButtonColor: "#003876",
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
            } else {
              return;
            }
          });
      }
    },
  },
};
</script>

<style scoped></style>
