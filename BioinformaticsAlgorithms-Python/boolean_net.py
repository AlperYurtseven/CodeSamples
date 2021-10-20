
class BooleanNet:
    def __init__(self):
        self.states = []
        self.calculated_list = []
        self.nodes = {}

    def importation(self, relations):

        self.states.append(relations)

    def dic_of_nodes(self):
        for i in range(len(self.states[0])):
            temp = []
            for j in range(len(self.states)):
                temp.append(self.states[j][i])

            self.nodes[i] = temp


    def integer_to_state(self,number):
        len_states = len(self.states)
        max_number = 2**len_states
        input_cond_list = [0] * len_states

        for i in range(len(self.states),0,-1):
            if number > 2**i:
                input_cond_list[i] = 1
                number = number-2**i

        if number == 1:
            input_cond_list[0] = 1

        return input_cond_list

    def run_network(self,first_cond):
        self.dic_of_nodes()
        first_state = self.integer_to_state(first_cond)

        self.calculated_list.append(first_state)
        count = 1

        while 2021:
            curr_list = [0] * len(self.calculated_list[0])
            for i in range(len(curr_list)):
                for j in range(len(self.nodes[i])):
                    if self.nodes[i][j] == -1:
                        if self.calculated_list[count-1][j] == 1:
                            curr_list[i] = 0
                            break
                    elif self.nodes[i][j] == 1:
                        if self.calculated_list[count-1][j] == 1:
                            curr_list[i] = 1
            if curr_list in self.calculated_list:
                break
            self.calculated_list.append(curr_list)
            count += 1

        for i in self.calculated_list:
            print(i)


