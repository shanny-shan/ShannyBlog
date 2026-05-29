<script setup>
import BugDialog from '@/views/_components/dialog/BugDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore, useArticleStore } from '@/stores'

const adminStore = useAdminStore()
const articleStore = useArticleStore()
const bugList = ref([])
const getBugList = async () => {
  const res = await articleStore.getArticleByTypeList('ARTICLE_BUG')
  if (res.data.code.toLowerCase() === 'success') {
    bugList.value = res.data.data || []
  }
}
onMounted(() => {
  getBugList()
})
</script>
<template>
  <div>
    <div class="flex justify-end">
      <button class="btn btn-primary" @click="adminStore.openDialog('bug')">
        Add Bug
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
            <th>Content</th>
            <th>Tags</th>
            <th>Published</th>
            <th>CreateTime</th>
            <th>UpdateTime</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in bugList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.title }}</td>
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
            <td>{{ item.createTime }}</td>
            <td>{{ item.updateTime }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs">Edit</button>
                <button class="btn btn-ghost btn-xs">Delete</button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <BugDialog />
</template>
<style lang="scss" scoped></style>
