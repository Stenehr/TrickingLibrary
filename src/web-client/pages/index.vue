<template>
    <v-row justify="center" align="center">
        <v-col cols="12" sm="8" md="6">
            <div v-if="tricks">
                <p :key="indx" v-for="(t, indx) in tricks">
                    {{ t.name }}
                </p>
            </div>
            <div>
                <v-text-field label="Trick name" v-model="trickName" />
                <v-btn @click="saveTrick">Save trick</v-btn>
            </div>
            {{ message }}
            <v-btn @click="reset">Reset</v-btn>
            <v-btn @click="resetTricks">ResetTricks</v-btn>
        </v-col>
    </v-row>
</template>

<script>
import { mapState, mapActions, mapMutations } from "vuex";

export default {
    data: () => ({
        trickName: ""
    }),

    computed: {
        ...mapState({
            message: state => state.message
        }),
        ...mapState("tricks", {
            tricks: state => state.tricks
        })
    },

    methods: {
        ...mapMutations(["reset"]),
        ...mapMutations("tricks", {
            resetTricks: "reset"
        }),
        ...mapActions("tricks", ["createTrick"]),
        async saveTrick() {
            await this.createTrick({ trick: { name: this.trickName } });
            this.trickName = "";
        }
    }

    // async fetch() {
    //     await this.$store.dispatch("fetchMessage")
    // }
    // data: () => ({
    //     message: "TEST"
    // }),
    // async asyncData(payload) {
    //     const { data } = await Axios.get("http://localhost:5000/api/home");
    //     return {
    //         message: data
    //     };
    // }
};
</script>
