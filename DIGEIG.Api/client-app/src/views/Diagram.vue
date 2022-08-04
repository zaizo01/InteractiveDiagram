<template>
  <div class="m-4">
    <div class="d-flex justify-content-start w-25 my-2">
      <b-button-group class="w-50 text-start" size="sm">
        <b-button
          @click="controlScale('bigger')"
          variant="secondary"
          class="text-white fw-bold"
          >+</b-button
        >
        <b-button
          @click="controlScale('smaller')"
          variant="secondary"
          class="text-white fw-bold"
          >-</b-button
        >
        <b-button
          @click="controlScale('restore')"
          variant="secondary"
          class="text-white fw-bold"
          >Reset</b-button
        >
      </b-button-group>
    </div>
    <div>
      <!-- 1320px -->
      <vue-tree
        ref="scaleTree"
        style="width: 100%; height: 570px; border: 1px solid gray"
        :collapse-enabled="false"
        :dataset="institution"
        :config="treeConfig"
      >
        <template v-slot:node="{ node, collapsed = false }">
          <b-modal :ref="node.Name" 
                   centered ok-only button-size="sm" 
                   size="lg" id="modal-scrollable" 
                   scrollable
                   :header-bg-variant="headerBgVariant"
                   :header-text-variant="headerTextVariant">
              <template #modal-title class="model-title">
                  <h5>{{ node.Name }}</h5>
              </template>
            <template #modal-footer="{ close }">
              <b-button size="sm" variant="danger" @click="close()">
                Cerrar
              </b-button>
            </template>
            <template #default="">
              <p v-html="node.Description" class="my-4"></p>
            </template>
          </b-modal>
          <div
            class="rich-media-node"
            :style="{ border: collapsed ? '2px solid grey' : '' }"
          >
            <p
              style="font-weight: bold; font-size: x-small"
              class="text-center mt-3 w-100"
              @click="showModal(node.Name)"
            >
              {{ node.Name }}
            </p>
          </div>
        </template>
      </vue-tree>
    </div>
  </div>
</template>

<script>
import {
  getInstitutionStructureById,
  getDiagramStructureRecords,
} from "../api/Institution/InstitutionStructureService";
import { uuid } from "../utilities/utils";

export default {
  name: "Diagram",
  data() {
    return {
      institution: {},
      treeConfig: { nodeWidth: 250, nodeHeight: 0, levelHeight: 350 },
      headerBgVariant: 'primary',
      headerTextVariant: 'light',
    };
  },
  created() {
    this.getInstitution();
    this.getDiagramRecords();
  },
  updated() {
    for (var i = 0; i < 10; i++) {
      this.controlScale("smaller");
    }
  },
  methods: {
    async getInstitution() {
      const response = await getInstitutionStructureById(
        "cda9f076-7162-448f-bcfe-2f1122f92a81"
      );
      this.addUniqueKey(response.data);
      return response;
    },
    async getDiagramRecords() {
      const response = await getDiagramStructureRecords(this.$route.params.id);
      this.addUniqueKey(response.data);
      this.institution = response.data;
      const childs = response.data.ListInstitutionsStructure;
      const children = { children: childs };
      const childWithKeys = this.addUniqueKey(children);
      Object.assign(this.institution, childWithKeys);
      childs.map((child) => {
        if (child.MainInstitutionStructureId !== 0) {
          child.ListInstitutionsStructure.map((subChild) => {
            Object.assign(child, { children: child.ListInstitutionsStructure });
            Object.assign(subChild, {
              children: subChild.ListInstitutionsStructure,
            });
            subChild.ListInstitutionsStructure.map((lastSubChild) => {
              Object.assign(lastSubChild, {
                children: lastSubChild.ListInstitutionsStructure,
              });
            });
            this.addUniqueKey(subChild);
            this.addUniqueKey(child);
          });
        }
      });
      return response;
    },
    addUniqueKey(rootNode) {
      const queue = [rootNode];
      while (queue.length !== 0) {
        const node = queue.pop();
        node._key = uuid();
        if (node.children) {
          queue.push(...node.children);
        }
      }
      return rootNode;
    },
    controlScale(command) {
      switch (command) {
        case "bigger":
          this.$refs.scaleTree.zoomIn();
          break;
        case "smaller":
          this.$refs.scaleTree.zoomOut();
          break;
        case "restore":
          this.$refs.scaleTree.restoreScale();
          break;
      }
    },
    showModal(nodeName) {
      this.$refs[`${nodeName}`].show();
    },
  },
};
</script>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.rich-media-node {
  margin: 0px 25px;
  width: 800px;
  padding: 8px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  justify-content: center;
  color: #f9f9f9;
  background-color: #003876;
  border-radius: 4px;
}

.input {
  font-size: 16px;
  font-size: max(16px, 1em);
  font-family: inherit;
  padding: 0.25em 0.5em;
  background-color: #fff;
  border: 2px solid var(--input-border);
  border-radius: 4px;
}

.text-size-required {
  font-size: 12px;
}

.group-btns-colors {
  background-color: #003876;
}

  .modal-header {
        background: black;
        color: white;
    }
</style>
