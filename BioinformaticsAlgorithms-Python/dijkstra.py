import sys

"""
The user gives distance matrix to program, 1st row & fist column is name of the vertices.
If there is edge, weight of the edge, if there is not 0
and also, the user should give start and the end nodes so program can return shortest distance between two
"""

class Dijkstra:
    def __init__(self, adj_matrix_path):
        self.vertices = []
        self.adj_matrix = []
        self.vertices_dist_dict = {}
        self.neighbor_nodes = {}
        self.vertices_numeral = {}

        with open(adj_matrix_path) as infile:
            lines = infile.readlines()
        for i in range(len(lines)):
            temp_list = []
            splitted = lines[i].split()
            if i == 0:
                for j in range(len(splitted)):
                    self.vertices.append(splitted[j].rstrip())
            else:
                for j in range(1,len(splitted)):
                    temp_list.append(int(splitted[j].rstrip()))
                self.vertices_dist_dict[splitted[0]] = temp_list
                self.adj_matrix.append(temp_list)

        for i in range(len(self.vertices)):
            self.vertices_numeral[self.vertices[i]] = i

        for i in range(len(self.vertices)):
            self.vertices[i] = i

        for i in range(len(self.vertices)):
            temp_list = {}
            for j in range(len(self.adj_matrix[i])):
                if not self.adj_matrix[i][j] == 0:
                    temp_list[self.vertices[j]] = self.adj_matrix[i][j]

            self.neighbor_nodes[i] = temp_list


    def find_min_non_zero(self, liste):
        min_val = sys.maxsize
        idx = -1

        for i in range(len(liste)):
            if liste[i] != 0:
                if liste[i] < min_val:
                    min_val = liste[i]
                    idx = i

        return idx

    def dijkstra_shortest_path(self, node_a, node_b):
        """
        Dijkstra shortest path algorithm

        :param from which node
        :param to which node
        :return: shortest path if exist else returns inf

        """

        distance_list = [sys.maxsize] * len(self.vertices)
        previous_list = [""] * len(self.vertices)

        node_a_idx = self.vertices_numeral[node_a]
        node_b_idx = self.vertices_numeral[node_b]

        q = []
        q.append(node_a_idx)
        distance_list[node_a_idx] = 0

        visited = []

        while len(q) > 0:
            selected = q[0]
            if selected in visited:
                q.pop(0)
                continue

            for key in self.neighbor_nodes[selected].keys():
                if self.neighbor_nodes[selected][key] + distance_list[selected] < distance_list[key]:
                    distance_list[key] = self.neighbor_nodes[selected][key] + distance_list[selected]
                q.append(key)

            visited.append(selected)


        print("Shortest distance from " + str(node_a) + " to " + str(node_b) + " is: " + str(distance_list[node_b_idx]))











