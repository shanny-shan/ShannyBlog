<script setup>
import TagDialog from '@/views/_components/dialog/TagDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore } from '@/stores/modules/admin'
import { useTagStore } from '@/stores/modules/tag'
const adminStore = useAdminStore()
const tagStore = useTagStore()
const tagList = ref([])

const getTagList = async () => {
  const res = await tagStore.getTagList()
  console.log(res.data.data)
  if (res.data.code.toLowerCase() === 'success') {
    tagList.value = res.data.data || []
  }
}
onMounted(() => {
  getTagList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('tag')">
        Add tag
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
            <th>Name</th>
            <th>NameEn</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in tagList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.name }}</td>
            <td>{{ item.nameEn }}</td>
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
  <TagDialog />
</template>
<style lang="scss" scoped></style>
