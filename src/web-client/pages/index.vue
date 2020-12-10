<template>
    <div>
        <div v-if="tricks">
            <div :key="indx" v-for="(t, indx) in tricks">
                {{ t.name }}
                <div>
                    <video
                        controls
                        :src="`http://localhost:5000/api/videos/${t.video}`"
                    ></video>
                </div>
            </div>
        </div>

        
    </div>
</template>

<script>
import { mapState } from "vuex";

export default {
    computed: mapState("tricks", ["tricks"]),
};
</script>
