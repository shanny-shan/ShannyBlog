<script setup>
import ProjectDialog from '@/views/_components/dialog/ProjectDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore, useArticleStore, useSiteStore } from '@/stores'
const adminStore = useAdminStore()
const articleStore = useArticleStore()
const siteStore = useSiteStore()

const projectList = ref([])
const getProjectList = async () => {
  siteStore.loading = true
  const res = await articleStore.getArticleByTypeList('ARTICLE_PROJECT')
  if (res.data.code.toLowerCase() === 'success') {
    projectList.value = res.data.data || []
    siteStore.loading = false
  }
}

const editProject = (item) => {
  adminStore.openDialog('project')
  articleStore.articleForm = item
  adminStore.isEdit = true
}
const deleteProject = (item) => {}

onMounted(() => {
  getProjectList()
})
</script>
<template>
  <div>
    <div class="flex justify-end">
      <button class="btn btn-primary" @click="adminStore.openDialog('project')">
        Add Project
      </button>
    </div>
    <div class="overflow-x-auto mt-2">
      <table class="table">
        <thead>
          <tr>
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <th>Title</th>
            <th>Memo</th>
            <th>Content</th>
            <th>Tags</th>
            <th>Published</th>
            <!-- <th>CreateTime</th> -->
            <th>UpdateTime</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in projectList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.title }}</td>
            <td>{{ item.memo }}</td>
            <td>{{ item.content }}</td>
            <td>
              <div class="flex flex-wrap gap-2">
                <div
                  v-for="tag in item.tagList"
                  :key="tag.id"
                  class="badge badge-soft badge-primary"
                >
                  {{ tag.name }}
                </div>
              </div>
            </td>
            <td>{{ item.published }}</td>
            <!-- <td>{{ item.createTime }}</td> -->
            <td>{{ item.updateTime.substring(0, 10) }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs" @click="editProject(item)">
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="deleteProject(item)"
                >
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <ProjectDialog />
</template>
<style lang="scss" scoped></style>
